using AutoMapper;
using MovieApp.Domain.Entities;
using MovieApp.Domain.Models;

namespace MovieApp.API.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserSlimDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
            CreateMap<User, UserDto>();
            CreateMap<RegisterRequest, User>();
            CreateMap<User, RegisterResponse>();
            CreateMap<User, LoginResponse>();
        }
    }
}
