using MovieApp.Domain.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public class NewsletterHttpService : INewsletterHttpService
    {
        private readonly HttpClient _httpClient;

        public NewsletterHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<NewsletterSubscribeResponse> SubscribeToNewsletter(NewsletterSubscribeRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
