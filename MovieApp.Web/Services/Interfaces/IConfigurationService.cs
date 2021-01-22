using MovieApp.Web.Models;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public interface IConfigurationService
    {
        Task<Configuration> GetApiConfigurationAsync();
    }
}
