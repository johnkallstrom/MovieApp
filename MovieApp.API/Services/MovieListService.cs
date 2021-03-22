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

        public async Task<DeleteMovieItemResponse> DeleteMovieItemAsync(int movieListId, DeleteMovieItemRequest request)
        {
            var movieList = await _context.MovieLists.FirstOrDefaultAsync(list => list.Id == movieListId);

            var movieToRemove = movieList.Movies.FirstOrDefault(item => item.TmdbId == request.TmdbId);
            if (movieToRemove is null)
            {
                throw new MovieExistsException($"The movie with ID: {request.TmdbId} does not exist in this list.");
            }

            movieList.Movies.Remove(movieToRemove);

            _context.MovieLists.Update(movieList);
            _context.SaveChanges();

            var response = new DeleteMovieItemResponse();
            response.Success = true;
            response.Message = $"Successfully removed {movieToRemove.Title} from the list.";

            return response;
        }

        public async Task<AddMovieItemResponse> AddMovieItemAsync(int movieListId, AddMovieItemRequest request)
        {
            var movieList = await _context.MovieLists.FirstOrDefaultAsync(list => list.Id == movieListId);
            if (movieList.Movies.Any(item => item.TmdbId == request.TmdbId))
            {
                throw new MovieExistsException($"{request.Title} has already been added to this list.");
            }

            var movie = _mapper.Map<MovieItem>(request);
            movie.MovieListId = movieListId;

            movieList.Movies.Add(movie);

            _context.MovieLists.Update(movieList);
            _context.SaveChanges();

            var response = _mapper.Map<AddMovieItemResponse>(movie);
            response.Success = true;
            response.Message = $"Successfully added {response.Title} to the list.";

            return response;
        }

        public UpdateMovieListResponse UpdateMovieList(MovieList movieList)
        {
            if (movieList is null) throw new ArgumentNullException(nameof(movieList));

            _context.MovieLists.Update(movieList);
            _context.SaveChanges();

            var response = _mapper.Map<UpdateMovieListResponse>(movieList);
            response.Success = true;
            response.Message = "Update successful.";

            return response;
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

        public void DeleteMovieList(MovieList movieList)
        {
            if (movieList is null) throw new ArgumentNullException(nameof(movieList));

            var items = _context.MovieItems.Where(x => x.MovieListId == movieList.Id);

            foreach (var item in items)
            {
                _context.MovieItems.Remove(item);
            }

            _context.MovieLists.Remove(movieList);
            _context.SaveChanges();
        }

        public async Task<MovieList> GetMovieListAsync(int movieListId)
        {
            var movieList = await _context.MovieLists
                .Include(x => x.Movies)
                .FirstOrDefaultAsync(x => x.Id == movieListId);

            return movieList;
        }

        public async Task<IEnumerable<MovieList>> GetMovieListsAsync(int userId)
        {
            var movieLists = await _context.MovieLists
                .Where(x => x.UserId == userId)
                .ToListAsync();

            return movieLists;
        }
    }
}
