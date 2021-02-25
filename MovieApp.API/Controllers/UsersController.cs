﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Domain.Entities;
using MovieApp.Domain.Exceptions;
using MovieApp.Domain.Models;
using MovieApp.API.Services;
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

        [HttpGet("hello")]
        [AllowAnonymous]
        public IActionResult Hello() => Ok(new { Message = "Hello World!" });

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponse>> LoginUser(LoginRequest request)
        {
            try
            {
                var response = await _userService.LoginUserAsync(request);
                return response;
            }
            catch (InvalidUserException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<RegisterResponse>> RegisterUser(RegisterRequest request)
        {
            try
            {
                var response = await _userService.RegisterUserAsync(request);
                return Ok(response);
            }
            catch (EmailExistsException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _userService.GetUsersAsync();

            return Ok(_mapper.Map<IEnumerable<UserDto>>(users));
        }

        [HttpGet("{userId}")]
        [AllowAnonymous]
        public async Task<ActionResult<User>> GetUser(int userId)
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
