using MovieApp.Web.Models;
using MovieApp.Web.Parameters;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public interface ISearchService
    {
        Task<SearchResults> GetMultiSearchAsync(SearchParameters parameters);
    }
}
