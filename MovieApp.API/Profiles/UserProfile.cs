using AutoMapper;

namespace MovieApp.API.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Entities.User, Models.UserDto>();
            CreateMap<Models.RegisterRequest, Entities.User>();
            CreateMap<Entities.User, Models.RegisterResponse>();
            CreateMap<Entities.User, Models.LoginResponse>();
        }
    }
}
