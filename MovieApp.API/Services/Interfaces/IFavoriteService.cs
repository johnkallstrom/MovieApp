using MovieApp.Domain.Entities;
using MovieApp.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.API.Services
{
    public interface IFavoriteService
    {
        Task AddFavoriteMovie(int userId, FavoriteMovie movie);
        Task<FavoriteMovie> GetFavoriteMovie(int tmdbId, int userId);
        Task<IEnumerable<FavoriteMovie>> GetFavoriteMovies(int userId);
        void DeleteFavoriteMovie(FavoriteMovie movie);
    }
}
