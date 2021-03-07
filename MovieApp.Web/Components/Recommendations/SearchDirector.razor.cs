using Microsoft.AspNetCore.Components;
using MovieApp.Web.Models;
using MovieApp.Web.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.Recommendations
{
    public partial class SearchDirector
    {
        [Inject]
        public IPeopleHttpService PeopleService { get; set; }

        public Person SelectedDirector { get; set; }

        [Parameter]
        public EventCallback<Person> OnDirectorSelected { get; set; }

        public void ClearDirector() => SelectedDirector = null;

        private async Task<IEnumerable<Person>> SearchDirectors(string searchText)
        {
            var data = await PeopleService.GetPeopleBySearchAsync(searchText);

            return data.Results.Where(x => x.Known_For_Department == "Directing").ToList();
        }

        private async Task HandleDirectorSelection(Person result)
        {
            ClearDirector();
            SelectedDirector = result;
            await OnDirectorSelected.InvokeAsync(SelectedDirector);
        }
    }
}
