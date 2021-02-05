using MovieApp.Web.Enums;
using MovieApp.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public interface ITrendingService
    {
        Task<IEnumerable<Media>> GetTrendingItemsAsync(string mediaType, TimeWindowType timeWindowType);
    }
}
