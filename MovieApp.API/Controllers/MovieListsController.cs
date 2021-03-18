using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieApp.API.Services;
using MovieApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.API.Controllers
{
    [Route("api/lists")]
    [ApiController]
    public class MovieListsController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMovieListService _movieListService;
        private readonly IMapper _mapper;

        public MovieListsController(
            IUserService userService,
            IMovieListService movieListService,
            IMapper mapper)
        {
            _userService = userService;
            _movieListService = movieListService;
            _mapper = mapper;
        }

        [HttpPost("{movieListId}/add")]
        public async Task<ActionResult<AddMovieResponse>> AddMovieListItem(int movieListId, AddMovieRequest request)
        {
            var response = new AddMovieResponse();

            var movieList = await _movieListService.GetMovieListAsync(movieListId);
            if (movieList is null) return NotFound(new { Message = "The list you are requesting does not exist." });

            try
            {
                response = await _movieListService.AddMovieAsync(movieListId, request);

                return Ok(response);
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message;

                return BadRequest(response);
            }
        }

        [HttpGet("{movieListId}")]
        public async Task<ActionResult<MovieListDto>> GetMovieList(int movieListId)
        {
            var movieList = await _movieListService.GetMovieListAsync(movieListId);
            if (movieList is null) return NotFound(new { Message = "The list you are requesting does not exist." });

            return Ok(_mapper.Map<MovieListDto>(movieList));
        }

        [HttpPost("{userId}")]
        public async Task<ActionResult<CreateMovieListResponse>> CreateMovieList(int userId, CreateMovieListRequest request)
        {
            var response = new CreateMovieListResponse();

            var user = await _userService.GetUserAsync(userId);

            if (user is null)
            {
                response.Success = false;
                response.Message = "The user does not exist in our database.";

                return NotFound(response);
            }

            try
            {
                response = await _movieListService.CreateMovieListAsync(userId, request);

                return Ok(response);
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message;

                return BadRequest(response);
            }
        }

        [HttpPut("{movieListId}")]
        public async Task<ActionResult<UpdateMovieListResponse>> UpdateMovieList(int movieListId, UpdateMovieListRequest request)
        {
            var response = new UpdateMovieListResponse();

            var movieList = await _movieListService.GetMovieListAsync(movieListId);
            if (movieList is null)
            {
                response.Success = false;
                response.Message = "The list you are requesting does not exist.";

                return NotFound(response);
            }

            try
            {
                _mapper.Map(request, movieList);
                response = _movieListService.UpdateMovieList(movieList);

                return Ok(response);
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.Success = false;

                return BadRequest(response);
            }
        }

        [HttpDelete("{movieListId}")]
        public async Task<ActionResult> DeleteMovieList(int movieListId)
        {
            var movieList = await _movieListService.GetMovieListAsync(movieListId);
            if (movieList is null) return NotFound("The list you are requesting does not exist.");

            _movieListService.DeleteMovieList(movieList);

            return NoContent();
        }
    }
}
