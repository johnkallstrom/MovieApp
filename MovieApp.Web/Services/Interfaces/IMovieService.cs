using MovieApp.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>> GetSimilarMoviesAsync(int movieId);
        Task<IEnumerable<Person>> GetMovieCastAsync(int movieId);
        Task<MovieDetails> GetMovieDetailsAsync(int movieId);
        Task<MovieResults> GetPopularMoviesAsync();
        Task<MovieResults> GetPopularMoviesAsync(int page);
        Task<IEnumerable<Movie>> GetTopRatedMoviesAsync();
        Task<MovieResults> GetUpcomingMoviesAsync();
        Task<MovieResults> GetUpcomingMoviesAsync(int page);
    }
}