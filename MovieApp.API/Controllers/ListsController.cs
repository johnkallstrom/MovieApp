using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieApp.API.Services;
using MovieApp.Domain.Entities;
using MovieApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.API.Controllers
{
    [Route("api/lists")]
    [ApiController]
    public class ListsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IListService _listService;

        public ListsController(
            IMapper mapper,
            IUserService userService,
            IListService listService)
        {
            _mapper = mapper;
            _userService = userService;
            _listService = listService;
        }

        [HttpPost("create/{userId}")]
        public async Task<IActionResult> CreateList(int userId, CreateListDto model)
        {
            bool userExists = await _userService.UserExistsAsync(userId);

            if (!userExists)
            {
                return NotFound();
            }

            try
            {
                var list = _mapper.Map<List>(model);
                list.UserId = userId;
                
                await _listService.CreateAsync(list);

                return CreatedAtRoute(nameof(GetUserLists), new { userId = userId }, _mapper.Map<ListDto>(list));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{userId}", Name = nameof(GetUserLists))]
        public async Task<ActionResult<IEnumerable<ListDto>>> GetUserLists(int userId)
        {
            bool userExists = await _userService.UserExistsAsync(userId);

            if (!userExists)
            {
                return NotFound();
            }

            var lists = await _listService.GetListsByUserAsync(userId);

            return Ok(_mapper.Map<IEnumerable<ListDto>>(lists));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ListDto>>> GetAllLists()
        {
            var lists = await _listService.GetAllListsAsync();

            return Ok(_mapper.Map<IEnumerable<ListDto>>(lists));
        }
    }
}
