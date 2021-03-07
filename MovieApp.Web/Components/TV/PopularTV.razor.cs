using Microsoft.AspNetCore.Components;
using MovieApp.Web.Models;
using MovieApp.Web.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.TV
{
    public partial class PopularTV
    {
        [Inject]
        public ITVHttpService TVService { get; set; }

        public IEnumerable<TVShow> Results { get; set; } = new List<TVShow>();

        public int Page { get; set; } = 1;
        public int TotalPages { get; set; }
        public int TotalResults { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var data = await TVService.GetPopularTVAsync(Page);

            SetTVData(data);
        }

        protected async Task HandlePageChanged(int selectedPage)
        {
            Page = selectedPage;

            var data = await TVService.GetPopularTVAsync(Page);

            SetTVData(data);
        }

        private void SetTVData(TVResults data)
        {
            if (data is not null)
            {
                Results = data.Results;
                Page = data.Page;
                TotalPages = data.Total_Pages;
                TotalResults = data.Total_Results;
            }
        }
    }
}
