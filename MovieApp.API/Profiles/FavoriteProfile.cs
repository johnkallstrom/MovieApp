using AutoMapper;
using MovieApp.Domain.Entities;
using MovieApp.Domain.Models;

namespace MovieApp.API.Profiles
{
    public class FavoriteProfile : Profile
    {
        public FavoriteProfile()
        {
            CreateMap<AddFavoriteMovieDto, FavoriteMovie>();
            CreateMap<FavoriteMovie, FavoriteMovieDto>();
        }
    }
}
