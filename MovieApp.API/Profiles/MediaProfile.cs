using AutoMapper;
using MovieApp.Domain.Entities;
using MovieApp.Domain.Models;

namespace MovieApp.API.Profiles
{
    public class MediaProfile : Profile
    {
        public MediaProfile()
        {
            CreateMap<MediaList, MediaListDto>();
            CreateMap<CreateMediaListDto, MediaList>();
            CreateMap<MediaDto, Media>();
            CreateMap<Media, MediaDto>();
        }
    }
}
