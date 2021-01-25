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

        public IEnumerable<Person> People { get; set; } = new List<Person>();

        protected override async Task OnInitializedAsync()
        {
            People = await PeopleService.GetPopularPeopleAsync();
        }
    }
}
