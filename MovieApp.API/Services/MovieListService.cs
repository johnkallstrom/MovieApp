using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieApp.API.Data;
using MovieApp.Domain.Entities;
using MovieApp.Domain.Exceptions;
using MovieApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.API.Services
{
    public class MovieListService : IMovieListService
    {
        private readonly IMapper _mapper;
        private readonly MovieAppContext _context;

        public MovieListService(
            IMapper mapper,
            MovieAppContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<CreateMovieListResponse> CreateMovieListAsync(int userId, CreateMovieListRequest request)
        {
            if (_context.MovieLists.Where(x => x.UserId == userId).Any(x => x.Name == request.Name))
            {
                throw new NameExistsException("The name you entered is not available.");
            }

            var movieList = _mapper.Map<MovieList>(request);

            movieList.UserId = userId;
            movieList.Created = DateTime.Now;

            await _context.MovieLists.AddAsync(movieList);
            await _context.SaveChangesAsync();

            var response = _mapper.Map<CreateMovieListResponse>(movieList);
            response.Success = true;
            response.Message = "Successfully created a new list.";

            return response;
        }

        public async Task<MovieList> GetMovieListAsync(int userId, int movieListId)
        {
            var movieList = await _context.MovieLists
                .Where(x => x.UserId == userId)
                .FirstOrDefaultAsync(x => x.Id == movieListId);

            return movieList;
        }

        public async Task<IEnumerable<MovieList>> GetMovieListsAsync(int userId)
        {
            var movieLists = await _context.MovieLists
                .Where(x => x.UserId == userId)
                .OrderBy(x => x.Created)
                .ToListAsync();

            return movieLists;
        }
    }
}
