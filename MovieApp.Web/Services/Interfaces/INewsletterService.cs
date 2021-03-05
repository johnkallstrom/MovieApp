using MovieApp.Domain.Models;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public interface INewsletterService
    {
        Task<NewsletterSubscribeResponse> SubscribeToNewsletter(NewsletterSubscribeRequest request);
    }
}
