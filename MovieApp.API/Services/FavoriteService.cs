using Microsoft.EntityFrameworkCore;
using MovieApp.API.Data;
using MovieApp.Domain.Entities;
using MovieApp.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.API.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly MovieAppContext _context;

        public FavoriteService(MovieAppContext context)
        {
            _context = context;
        }

        public async Task AddFavoriteMovie(int userId, FavoriteMovie movie)
        {
            if (movie is null) throw new ArgumentNullException(nameof(movie));

            if (_context.FavoriteMovies.Where(x => x.UserId == userId).Any(x => x.TmdbId == movie.TmdbId))
            {
                throw new FavoriteMovieExistsException($"{movie.Title} with TMDB ID: {movie.TmdbId} already exists in the users favorite list.");
            }

            movie.UserId = userId;
            movie.Created = DateTime.Now;

            await _context.AddAsync(movie);
            await _context.SaveChangesAsync();
        }

        public async Task<FavoriteMovie> GetFavoriteMovie(int tmdbId, int userId)
        {
            var favoriteMovie = await _context.FavoriteMovies
                .Where(x => x.UserId == userId)
                .FirstOrDefaultAsync(x => x.TmdbId == tmdbId);

            return favoriteMovie;
        }

        public async Task<IEnumerable<FavoriteMovie>> GetFavoriteMovies(int userId)
        {
            return await _context.FavoriteMovies.Where(x => x.UserId == userId).ToListAsync();
        }

        public void DeleteFavoriteMovie(FavoriteMovie movie)
        {
            if (movie is null) throw new ArgumentNullException(nameof(movie));

            _context.FavoriteMovies.Remove(movie);
            _context.SaveChanges();
        }
    }
}
