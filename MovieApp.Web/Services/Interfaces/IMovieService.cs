using MovieApp.Web.Models;
using MovieApp.Web.Parameters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>> GetSimilarMoviesAsync(int movieId);
        Task<IEnumerable<Person>> GetMovieCastAsync(int movieId);
        Task<IEnumerable<Movie>> GetMoviesBySearchAsync(MovieParameters parameters);
        IEnumerable<Movie> GetMoviesBySearch(MovieParameters parameters);
        Task<MovieDetails> GetMovieDetailsAsync(int movieId);
        Task<IEnumerable<Movie>> GetPopularMoviesAsync();
        Task<IEnumerable<Movie>> GetTopRatedMoviesAsync();
        Task<IEnumerable<Movie>> GetUpcomingMoviesAsync();
    }
}