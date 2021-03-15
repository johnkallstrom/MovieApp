using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using MovieApp.Web.Helpers;

namespace MovieApp.Web.Components.User
{
    public partial class UserFavoriteList
    {
        private const string IMAGE_URL = "TMDB:PlaceholderImageBaseUrl";

        [Inject]
        public IConfiguration Config { get; set; }

        public string PlaceholderImageUrl { get; set; }

        protected override void OnInitialized()
        {
            PlaceholderImageUrl = ImageHelper.GetPlaceholderImageUrl(new ImageSettings(Config[IMAGE_URL], 500, 750));
        }
    }
}
