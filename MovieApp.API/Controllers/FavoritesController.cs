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
    [Route("/api/favorites")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IFavoriteService _favoriteService;

        public FavoritesController(
            IMapper mapper,
            IUserService userService,
            IFavoriteService favoriteService)
        {
            _mapper = mapper;
            _userService = userService;
            _favoriteService = favoriteService;
        }

        [HttpPost("movie/{userId}")]
        public async Task<ActionResult> AddFavoriteMovie(int userId, AddFavoriteMovieDto model)
        {
            var user = await _userService.GetUserAsync(userId);

            if (user is null) return NotFound(new { Message = "The user does not exist in our database." });

            try
            {
                var movieToAdd = _mapper.Map<FavoriteMovie>(model);

                await _favoriteService.AddFavoriteMovie(userId, movieToAdd);

                return Ok(new { Message = $"Succesfully added {movieToAdd.Title} to {user.FirstName} {user.LastName} favorite list." });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("movie/{userId}/{tmdbId}")]
        public async Task<ActionResult<FavoriteMovieDto>> GetFavoriteMovie(int userId, int tmdbId)
        {
            var user = await _userService.GetUserAsync(userId);
            if (user is null) return NotFound(new { Message = "The user does not exist in our database." });

            var favoriteMovie = await _favoriteService.GetFavoriteMovie(tmdbId, userId);
            if (favoriteMovie is null) return NotFound(new { Message = "The movie you are trying to get does not exist in the users favorite list." });

            return Ok(_mapper.Map<FavoriteMovieDto>(favoriteMovie));
        }

        [HttpGet("movie/{userId}")]
        public async Task<ActionResult<IEnumerable<FavoriteMovieDto>>> GetFavoriteMovies(int userId)
        {
            var user = await _userService.GetUserAsync(userId);

            if (user is null) return NotFound(new { Message = "The user does not exist in our database." });

            var favoriteMovies = await _favoriteService.GetFavoriteMovies(userId);

            return Ok(_mapper.Map<IEnumerable<FavoriteMovieDto>>(favoriteMovies));
        }

        [HttpDelete("movie/{userId}/{tmdbId}")]
        public async Task<ActionResult> DeleteFavoriteMovie(int userId, int tmdbId)
        {
            var user = await _userService.GetUserAsync(userId);
            if (user is null) return NotFound(new { Message = "The user does not exist in our database." });

            var favoriteMovie = await _favoriteService.GetFavoriteMovie(tmdbId, userId);
            if (favoriteMovie is null) return NotFound(new { Message = "The movie you are trying to delete does not exist in the users favorite list." });

            _favoriteService.DeleteFavoriteMovie(favoriteMovie);

            return NoContent();
        }
    }
}
