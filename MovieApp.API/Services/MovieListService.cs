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
    public class MovieListService : IMovieListService
    {
        private readonly MovieAppContext _context;

        public MovieListService(
            MovieAppContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(MovieList movieList)
        {
            if (movieList is null) throw new ArgumentNullException(nameof(movieList));

            bool nameExists = _context.MovieLists.Where(x => x.Id == movieList.UserId).Any(x => x.Name == movieList.Name);
            if (nameExists) throw new NameExistsException("The name you entered is not available.");

            await _context.MovieLists.AddAsync(movieList);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MovieList>> GetAsync(int userId)
        {
            return await _context.MovieLists
                .Where(x => x.UserId == userId)
                .Include(x => x.Movies)
                .Include(x => x.User)
                .ToListAsync();
        }
    }
}
