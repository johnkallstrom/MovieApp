using Microsoft.AspNetCore.Components;
using MovieApp.Web.Models;
using MovieApp.Web.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.People
{
    public partial class PopularPeople
    {
        [Inject]
        public IPeopleService PeopleService { get; set; }

        [Parameter]
        public string HeaderText { get; set; } = "Popular";

        public int Page { get; set; } = 1;
        public int TotalResults { get; set; }
        public int TotalPages { get; set; }
        public IEnumerable<Person> Results { get; set; } = new List<Person>();

        protected override async Task OnInitializedAsync()
        {
            var data = await PeopleService.GetPopularPeopleAsync(Page);

            SetPeopleData(data);
        }

        protected async Task SetPage(int page)
        {
            Page = page;

            var data = await PeopleService.GetPopularPeopleAsync(Page);

            SetPeopleData(data);
        }

        private void SetPeopleData(PeopleResults data)
        {
            if (data is not null)
            {
                Results = data.Results;
                TotalResults = data.Total_Results;
                TotalPages = data.Total_Pages;
            }
        }
    }
}
