using MovieApp.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>> GetMoviesBySearchAsync(string query);
        Task<MovieDetails> GetMovieDetailsAsync(int movieId);
        Task<IEnumerable<Movie>> GetPopularMoviesAsync();
        Task<IEnumerable<Movie>> GetTopRatedMoviesAsync();
        Task<IEnumerable<Movie>> GetUpcomingMoviesAsync();
    }
}