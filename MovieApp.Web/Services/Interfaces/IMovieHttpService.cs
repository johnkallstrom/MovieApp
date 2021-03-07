using MovieApp.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public interface IMovieHttpService
    {
        Task<IEnumerable<Movie>> GetSimilarMoviesAsync(int movieId);
        Task<IEnumerable<Person>> GetMovieCastAsync(int movieId);
        Task<MovieDetails> GetMovieDetailsAsync(int movieId);
        Task<MovieResults> GetPopularMoviesAsync();
        Task<MovieResults> GetPopularMoviesAsync(int page);
        Task<MovieResults> GetTopRatedMoviesAsync();
        Task<MovieResults> GetTopRatedMoviesAsync(int page);
        Task<MovieResults> GetUpcomingMoviesAsync();
        Task<MovieResults> GetUpcomingMoviesAsync(int page);
    }
}