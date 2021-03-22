using AutoMapper;
using MovieApp.Domain.Entities;
using MovieApp.Domain.Models;

namespace MovieApp.API.Profiles
{
    public class MovieItemProfile : Profile
    {
        public MovieItemProfile()
        {
            CreateMap<MovieItem, MovieItemDto>();
            CreateMap<MovieItem, AddMovieItemResponse>();
            CreateMap<AddMovieItemRequest, MovieItem>();
        }
    }
}
