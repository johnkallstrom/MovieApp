using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieApp.API.Services;
using MovieApp.Domain.Entities;
using MovieApp.Domain.Exceptions;
using MovieApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediaService _mediaListService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UsersController(
            IMediaService mediaListService,
            IMapper mapper,
            IUserService userService)
        {
            _mediaListService = mediaListService;
            _mapper = mapper;
            _userService = userService;
        }

        [HttpGet("{userId}/lists", Name = nameof(GetMediaLists))]
        public async Task<ActionResult> GetMediaLists(int userId)
        {
            bool userExists = await _userService.UserExists(userId);

            if (!userExists)
            {
                return NotFound(new { Message = $"There is no user in our database with the provided ID: {userId}" });
            }

            var mediaListEntities = await _mediaListService.GetMediaListsAsync(userId);

            var mediaListDtos = new List<MediaListDto>();

            foreach (var mediaListEntity in mediaListEntities)
            {
                mediaListDtos.Add(_mapper.Map<MediaListDto>(mediaListEntity));
            }

            return Ok(mediaListDtos);
        }

        [HttpPost("{userId}/list")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateMediaList(int userId, CreateMediaListDto model)
        {
            bool userExists = await _userService.UserExists(userId);

            if (!userExists)
            {
                return NotFound(new { Message = $"There is no user in our database with the provided ID: {userId}" });
            }
            
            try
            {
                var mediaListEntity = _mapper.Map<MediaList>(model);
                mediaListEntity.UserId = userId;

                await _mediaListService.CreateMediaListAsync(mediaListEntity);

                var mediaListDto = _mapper.Map<MediaListDto>(mediaListEntity);

                return CreatedAtRoute(nameof(GetMediaLists), new { userId = userId }, mediaListDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
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
        [AllowAnonymous]
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
