using MovieApp.Web.Models;
using MovieApp.Web.Parameters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public interface ISearchService
    {
        Task<IEnumerable<Media>> GetMultiSearchAsync(SearchParameters parameters);
    }
}
