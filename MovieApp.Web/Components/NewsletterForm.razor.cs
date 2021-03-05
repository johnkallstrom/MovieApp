using Microsoft.AspNetCore.Components;
using MovieApp.Domain.Models;
using MovieApp.Web.Services;
using System.Threading.Tasks;

namespace MovieApp.Web.Components
{
    public partial class NewsletterForm
    {
        [Inject]
        public INewsletterService NewsletterService { get; set; }

        public NewsletterSubscribeRequest NewsletterSubscribeModel { get; set; } = new NewsletterSubscribeRequest();

        public bool DisplayLoadingSpinner { get; set; }

        protected async Task HandleValidSubmit()
        {
            DisplayLoadingSpinner = true;

            var response = await NewsletterService.SubscribeToNewsletter(NewsletterSubscribeModel);

            if (response.Success)
            {
                DisplayLoadingSpinner = false;
            }
        }
    }
}
