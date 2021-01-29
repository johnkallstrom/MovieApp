using MovieApp.Web.Models;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public interface IApiConfigurationService
    {
        Task<ApiConfiguration> GetApiConfigurationAsync();
    }
}
