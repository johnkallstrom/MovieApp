﻿using AutoMapper;
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

        [HttpPost("{userId}/create")]
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

        [HttpGet("{userId}/{movieListId}")]
        public async Task<ActionResult<MovieListDto>> GetMovieList(int userId, int movieListId)
        {
            var user = await _userService.GetUserAsync(userId);
            if (user is null) return NotFound("The user does not exist in our database.");

            var movieList = await _movieListService.GetMovieListAsync(userId, movieListId);
            if (movieList is null) return NotFound("The list you are requesting does not exist.");

            return Ok(_mapper.Map<MovieListDto>(movieList));
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<MovieListDto>>> GetMovieLists(int userId)
        {
            var user = await _userService.GetUserAsync(userId);

            if (user is null) return NotFound("The user does not exist in our database");

            var movieLists = await _movieListService.GetMovieListsAsync(userId);

            return Ok(_mapper.Map<IEnumerable<MovieListDto>>(movieLists));
        }
    }
}