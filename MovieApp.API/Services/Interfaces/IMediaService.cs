using MovieApp.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.API.Services
{
    public interface IMediaService
    {
        Task<IEnumerable<MediaList>> GetMediaListsAsync(int userId);
        Task CreateMediaListAsync(MediaList mediaList);
    }
}
