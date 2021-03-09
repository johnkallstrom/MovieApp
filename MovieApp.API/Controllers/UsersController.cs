using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieApp.API.Services;
using MovieApp.Domain.Exceptions;
using MovieApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UsersController(
            IMapper mapper,
            IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        [HttpPost("{userId}/list")]
        public async Task CreateList(CreateListDto model)
        {
            throw new NotImplementedException();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponse>> LoginUser(LoginRequest request)
        {
            var response = new LoginResponse();

            try
            {
                response = await _userService.LoginUserAsync(request);
                return Ok(response);
            }
            catch (InvalidUserException e)
            {
                response.Message = e.Message;
                response.Success = false;

                return BadRequest(response);
            }
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<RegisterResponse>> RegisterUser(RegisterRequest request)
        {
            var response = new RegisterResponse();

            try
            {
                response = await _userService.RegisterUserAsync(request);
                return Ok(response);
            }
            catch (EmailExistsException e)
            {
                response.Message = e.Message;
                response.Success = false;

                return BadRequest(response);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await _userService.GetUsersAsync();

            return Ok(_mapper.Map<IEnumerable<UserDto>>(users));
        }

        [HttpGet("{userId}")]
        [AllowAnonymous]
        public async Task<ActionResult<UserDto>> GetUser(int userId)
        {
            var user = await _userService.GetUserAsync(userId);

            if (user is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UserDto>(user));
        }
    }
}
