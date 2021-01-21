using Microsoft.AspNetCore.Components;
using MovieApp.Web.Models;
using MovieApp.Web.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.People
{
    public partial class DetailsPerson
    {
        private const int BIO_COUNT = 500;

        public bool DisplayShortBiography { get; set; } = true;

        [Inject]
        public IPeopleService PeopleService { get; set; }

        [Parameter]
        public string Id { get; set; }

        public PersonDetails Person { get; set; } = new PersonDetails();

        public IEnumerable<Movie> Movies { get; set; } = new List<Movie>();

        protected override async Task OnInitializedAsync()
        {
            var movies = Enumerable.Empty<Movie>();

            if (int.TryParse(Id, out int personId))
            {
                Person = await PeopleService.GetPersonAsync(personId);
                Person.ShortBiography = GetShortBiography();
                movies = await PeopleService.GetPersonMoviesAsync(personId);
            }

            Movies = movies.Take(10);
        }

        protected void HandleReadMore()
        {
            if (DisplayShortBiography)
            {
                DisplayShortBiography = false;
            }
            else
            {
                DisplayShortBiography = true;
            }
        }

        protected string GetShortBiography()
        {
            string output = string.Empty;

            if (!string.IsNullOrWhiteSpace(Person.Biography))
            {
                output = Person.Biography.Substring(0, BIO_COUNT).TrimEnd();
            }

            return $"{output}...";
        }
    }
}
