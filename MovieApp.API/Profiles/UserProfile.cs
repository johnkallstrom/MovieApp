using AutoMapper;
using MovieApp.Domain.Entities;
using MovieApp.Domain.Models;

namespace MovieApp.API.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UpdateUserResponse>();
            CreateMap<UpdateUserRequest, User>();
            CreateMap<User, UserDto>();
            CreateMap<RegisterRequest, User>();
            CreateMap<User, RegisterResponse>();
            CreateMap<User, LoginResponse>();
        }
    }
}
