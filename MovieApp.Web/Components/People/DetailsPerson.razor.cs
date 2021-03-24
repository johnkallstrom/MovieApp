using Microsoft.AspNetCore.Components;
using MovieApp.Web.Extensions;
using MovieApp.Web.Models;
using MovieApp.Web.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.People
{
    public partial class DetailsPerson
    {
        [Inject]
        public IPeopleHttpService PeopleService { get; set; }

        [Parameter]
        public string Id { get; set; }

        public PersonDetails Person { get; set; } = new PersonDetails();

        public IEnumerable<Movie> Movies { get; set; } = new List<Movie>();
        public bool DisplayTruncatedBio { get; set; }
        public string TruncatedBiography { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var movies = Enumerable.Empty<Movie>();

            if (int.TryParse(Id, out int personId))
            {
                Person = await PeopleService.GetPersonAsync(personId);
                movies = await PeopleService.GetPersonMoviesAsync(personId);
            }

            Movies = movies.Take(10);
            
            if (Person.Biography.Length >= 500)
            {
                DisplayTruncatedBio = true;
                Person.TruncatedBiography = Person.Biography.Truncate(500).EnsureEndsWithDot();
            }
        }
    }
}
