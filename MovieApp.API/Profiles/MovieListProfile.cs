﻿using AutoMapper;
using MovieApp.Domain.Entities;
using MovieApp.Domain.Models;

namespace MovieApp.API.Profiles
{
    public class MovieListProfile : Profile
    {
        public MovieListProfile()
        {
            CreateMap<MovieListItem, MovieListItemDto>();
            CreateMap<MovieListItem, AddMovieResponse>();
            CreateMap<AddMovieRequest, MovieListItem>();
            CreateMap<UpdateMovieListRequest, MovieList>();
            CreateMap<MovieList, UpdateMovieListResponse>();
            CreateMap<MovieList, MovieListDto>();
            CreateMap<CreateMovieListRequest, MovieList>();
            CreateMap<MovieList, CreateMovieListResponse>();
        }
    }
}
