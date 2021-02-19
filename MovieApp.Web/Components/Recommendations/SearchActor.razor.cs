using Microsoft.AspNetCore.Components;
using MovieApp.Web.Models;
using MovieApp.Web.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.Recommendations
{
    public partial class SearchActor
    {
        [Inject]
        public IPeopleService PeopleService { get; set; }

        public Person SelectedActor { get; set; }

        [Parameter]
        public EventCallback<Person> OnActorSelected { get; set; }

        public void ClearActor() => SelectedActor = null;

        private async Task<IEnumerable<Person>> SearchActors(string searchText)
        {
            var data = await PeopleService.GetPeopleBySearchAsync(searchText);

            return data.Results.Where(x => x.Known_For_Department == "Acting").ToList();
        }

        private async Task HandleActorSelection(Person result)
        {
            SelectedActor = result;
            await OnActorSelected.InvokeAsync(SelectedActor);
        }
    }
}
