using MovieApp.Web.Enums;
using MovieApp.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public interface ITrendingHttpService
    {
        Task<IEnumerable<Media>> GetTrendingItemsAsync(string mediaType, string timeWindowType);
    }
}
