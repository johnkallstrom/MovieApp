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

        public async Task<DeleteMovieResponse> DeleteMovieAsync(int movieListId, DeleteMovieRequest request)
        {
            var movieList = await _context.MovieLists.FirstOrDefaultAsync(list => list.Id == movieListId);

            var movieToRemove = movieList.Items.FirstOrDefault(item => item.MovieId == request.MovieId);
            if (movieToRemove is null)
            {
                throw new MovieExistsException($"The movie with ID: {request.MovieId} does not exist in this list.");
            }

            movieList.Items.Remove(movieToRemove);

            _context.MovieLists.Update(movieList);
            _context.SaveChanges();

            var response = _mapper.Map<DeleteMovieResponse>(movieToRemove);
            response.Success = true;
            response.Message = $"Successfully removed {response.Name} from the list.";

            return response;
        }

        public async Task<AddMovieResponse> AddMovieAsync(int movieListId, AddMovieRequest request)
        {
            var movieList = await _context.MovieLists.FirstOrDefaultAsync(list => list.Id == movieListId);
            if (movieList.Items.Any(item => item.MovieId == request.MovieId))
            {
                throw new MovieExistsException($"{request.Name} has already been added to this list.");
            }

            var movie = _mapper.Map<MovieListItem>(request);
            movie.MovieListId = movieListId;

            movieList.Items.Add(movie);

            _context.MovieLists.Update(movieList);
            _context.SaveChanges();

            var response = _mapper.Map<AddMovieResponse>(movie);
            response.Success = true;
            response.Message = $"Successfully added {response.Name} to the list.";

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

            var items = _context.Items.Where(x => x.MovieListId == movieList.Id);

            foreach (var item in items)
            {
                _context.Items.Remove(item);
            }

            _context.MovieLists.Remove(movieList);
            _context.SaveChanges();
        }

        public async Task<MovieList> GetMovieListAsync(int movieListId)
        {
            var movieList = await _context.MovieLists
                .Include(x => x.Items)
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
