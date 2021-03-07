using MovieApp.Domain.Models;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public interface INewsletterHttpService
    {
        Task<NewsletterSubscribeResponse> SubscribeToNewsletter(NewsletterSubscribeRequest request);
    }
}
