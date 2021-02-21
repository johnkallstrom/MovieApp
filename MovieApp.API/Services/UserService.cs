﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieApp.API.Data;
using MovieApp.API.Entities;
using MovieApp.API.Exceptions;
using MovieApp.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace MovieApp.API.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly MovieAppDatabaseContext _context;

        public UserService(
            IMapper mapper,
            MovieAppDatabaseContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<LoginResponse> LoginUserAsync(LoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user is null) throw new InvalidUserException("The email you entered does not exist.");
            if (!BC.Verify(request.Password, user.PasswordHash)) throw new InvalidUserException("The password you entered is incorrect.");

            // To do: Generate JWT token

            var response = _mapper.Map<LoginResponse>(user);

            return response;
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
    }
}
