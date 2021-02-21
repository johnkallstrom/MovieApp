using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieApp.API.Entities;
using MovieApp.API.Models;
using MovieApp.API.Services;
using System.Collections.Generic;

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

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            var users = _userService.GetUsers();

            return Ok(_mapper.Map<UserDto>(users));
        }
    }
}
