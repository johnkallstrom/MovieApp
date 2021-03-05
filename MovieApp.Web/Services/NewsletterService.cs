using MovieApp.Domain.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public class NewsletterService : INewsletterService
    {
        private readonly HttpClient _httpClient;

        public NewsletterService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<NewsletterSubscribeResponse> SubscribeToNewsletter(NewsletterSubscribeRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
