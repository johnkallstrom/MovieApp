using Microsoft.AspNetCore.Components;
using MovieApp.Web.Models;
using MovieApp.Web.Services;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.People
{
    public partial class DetailsPerson
    {
        [Inject]
        public IPeopleService PeopleService { get; set; }

        [Parameter]
        public string Id { get; set; }

        public PersonDetails Person { get; set; } = new PersonDetails();

        protected override async Task OnInitializedAsync()
        {
            if (int.TryParse(Id, out int personId))
            {
                Person = await PeopleService.GetPersonAsync(personId);
            }
        }
    }
}
