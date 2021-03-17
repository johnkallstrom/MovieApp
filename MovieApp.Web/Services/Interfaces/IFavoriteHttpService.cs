using MovieApp.Domain.Models;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public interface IFavoriteHttpService
    {
        public Task<bool> AddMovieToFavorites(int userId, AddFavoriteMovieDto model);
        public Task<bool> MovieExistsAsFavorite(int userId, int tmdbId);
        public Task DeleteMovieFromFavorites(int userId, int tmdbId);
    }
}