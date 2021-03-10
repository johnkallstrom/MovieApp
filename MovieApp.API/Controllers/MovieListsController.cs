using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieApp.API.Services;
using MovieApp.Domain.Entities;
using MovieApp.Domain.Exceptions;
using MovieApp.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.API.Controllers
{
    [Route("/lists")]
    [ApiController]
    public class MovieListsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IMovieListService _movieListService;

        public MovieListsController(
            IMapper mapper,
            IUserService userService,
            IMovieListService movieListService)
        {
            _mapper = mapper;
            _userService = userService;
            _movieListService = movieListService;
        }

        [HttpPost("create/{userId}")]
        public async Task<IActionResult> CreateMovieList(int userId, CreateMovieListDto model)
        {
            bool userExists = await _userService.UserExistsAsync(userId);

            if (!userExists)
            {
                return NotFound();
            }

            try
            {
                var movieList = _mapper.Map<MovieList>(model);
                movieList.UserId = userId;

                await _movieListService.CreateAsync(movieList);

                return CreatedAtRoute(nameof(GetMovieLists), new { userId = userId }, _mapper.Map<MovieListDto>(movieList));
            }
            catch (NameExistsException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{userId}", Name = nameof(GetMovieLists))]
        public async Task<ActionResult<IEnumerable<MovieListDto>>> GetMovieLists(int userId)
        {
            bool userExists = await _userService.UserExistsAsync(userId);

            if (!userExists)
            {
                return NotFound();
            }

            var lists = await _movieListService.GetAsync(userId);

            return Ok(_mapper.Map<IEnumerable<MovieListDto>>(lists));
        }
    }
}
