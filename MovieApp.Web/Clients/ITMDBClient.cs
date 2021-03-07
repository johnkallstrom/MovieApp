using System.Threading.Tasks;

namespace MovieApp.Web.Clients
{
    public interface ITMDBClient
    {
        Task<TValue> GetData<TValue>(string requestUri);
    }
}
