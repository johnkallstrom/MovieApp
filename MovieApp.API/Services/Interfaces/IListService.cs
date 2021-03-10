using MovieApp.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.API.Services
{
    public interface IListService
    {
        Task CreateAsync(List list);
        Task<IEnumerable<List>> GetListsByUserAsync(int userId);
        Task<IEnumerable<List>> GetAllListsAsync();
    }
}
