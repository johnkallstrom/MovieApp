using AutoMapper;
using MovieApp.Domain.Entities;
using MovieApp.Domain.Models;

namespace MovieApp.API.Profiles
{
    public class ListProfile : Profile
    {
        public ListProfile()
        {
            CreateMap<List, ListDto>();
            CreateMap<CreateListDto, List>();
        }
    }
}
