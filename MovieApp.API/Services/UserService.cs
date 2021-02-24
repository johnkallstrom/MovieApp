using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MovieApp.API.Data;
using MovieApp.API.Entities;
using MovieApp.API.Exceptions;
using MovieApp.API.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace MovieApp.API.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly MovieAppDatabaseContext _context;

        public UserService(
            IConfiguration config,
            IMapper mapper,
            MovieAppDatabaseContext context)
        {
            _config = config;
            _mapper = mapper;
            _context = context;
        }

        public async Task<LoginResponse> LoginUserAsync(LoginRequest request)
        {
            var user = await AuthenticateUser(request.Email, request.Password);

            if (user is not null)
            {
                var token = GenerateJwtToken(user);

                var response = _mapper.Map<LoginResponse>(user);
                response.Token = token;

                return response;
            }

            return null;
        }

        public async Task<RegisterResponse> RegisterUserAsync(RegisterRequest request)
        {
            if (_context.Users.Any(u => u.Email == request.Email))
            {
                throw new EmailExistsException("The email you entered already exists.");
            }

            var user = _mapper.Map<User>(request);

            user.Created = DateTime.Now;
            user.PasswordHash = BC.HashPassword(request.Password);

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return _mapper.Map<RegisterResponse>(user);
        }

        public async Task<User> GetUserAsync(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            return user;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            var users = _context.Users;

            if (users is null) throw new ArgumentNullException(nameof(users));

            return await users.ToListAsync();
        }

        private async Task<User> AuthenticateUser(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user is null) throw new InvalidUserException("The email you entered does not exist.");
            if (!BC.Verify(password, user.PasswordHash)) throw new InvalidUserException("The password you entered is incorrect.");

            return user;
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim("id", user.Id.ToString()),
                new Claim("firstName", user.FirstName),
                new Claim("lastName", user.LastName),
                new Claim("email", user.Email)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = credentials
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
