using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieApp.API.Services;
using MovieApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.API.Controllers
{
    [Route("/api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMovieListService _movieListService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UsersController(
            IMovieListService movieListService,
            IMapper mapper,
            IUserService userService)
        {
            _movieListService = movieListService;
            _mapper = mapper;
            _userService = userService;
        }

        [HttpPut("{userId}")]
        [AllowAnonymous]
        public async Task<ActionResult<UpdateUserResponse>> UpdateUser(int userId, UpdateUserRequest request)
        {
            var response = new UpdateUserResponse();

            var user = await _userService.GetUserAsync(userId);

            if (user is null) return NotFound();

            try
            {
                _mapper.Map(request, user);
                response = _userService.UpdateUser(user);

                return Ok(response);
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.Success = false;

                return BadRequest(response);
            }
        }

        [HttpGet("{userId}/lists")]
        public async Task<ActionResult<IEnumerable<MovieListDto>>> GetUserMovieLists(int userId)
        {
            var user = await _userService.GetUserAsync(userId);

            if (user is null) return NotFound("The user does not exist in our database");

            var movieLists = await _movieListService.GetMovieListsAsync(userId);

            return Ok(_mapper.Map<IEnumerable<MovieListDto>>(movieLists));
        }

        [HttpDelete("{userId}")]
        [AllowAnonymous]
        public async Task<ActionResult> DeleteUser(int userId)
        {
            var user = await _userService.GetUserAsync(userId);

            if (user is null) return NotFound();

            _userService.DeleteUser(user);

            return NoContent();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponse>> LoginUser(LoginRequest request)
        {
            var response = new LoginResponse();

            try
            {
                response = await _userService.LoginAsync(request);
                return Ok(response);
            }
            catch (Exception e)
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
                response = await _userService.RegisterAsync(request);
                return Ok(response);
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.Success = false;

                return BadRequest(response);
            }
        }

        [HttpGet("{userId}")]
        [AllowAnonymous]
        public async Task<ActionResult<UserDto>> GetUser(int userId)
        {
            var user = await _userService.GetUserAsync(userId);

            if (user is null)
            {
                return NotFound( new { Message = "The user does not exist in our database." });
            }

            return Ok(_mapper.Map<UserDto>(user));
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await _userService.GetUsersAsync();

            return Ok(_mapper.Map<IEnumerable<UserDto>>(users));
        }
    }
}
