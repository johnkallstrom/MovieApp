using Microsoft.AspNetCore.Components;
using MovieApp.Web.Data;
using MovieApp.Web.Models;
using MovieApp.Web.Parameters;
using MovieApp.Web.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.Recommendations
{
    public partial class MovieRecommendations
    {
        [Inject]
        public IPeopleService PeopleService { get; set; }

        [Inject]
        public IDiscoverService DiscoverService { get; set; }

        [Inject]
        public IGenreService GenreService { get; set; }

        public int Page { get; set; } = 1;
        public string SortOrder { get; set; }
        public int ReleaseYear { get; set; } = 0;
        public IEnumerable<Movie> Results { get; set; } = new List<Movie>();
        public int TotalPages { get; set; }
        public int TotalResults { get; set; }
        public string FromReleasedate { get; set; }
        public string ToReleaseDate { get; set; }
        public int Runtime { get; set; }
        public SearchActor SearchActorComponent { get; set; }
        public SearchDirector SearchDirectorComponent { get; set; }
        public SearchKeyword SearchKeywordComponent { get; set; }
        public List<int> SelectedGenreIds { get; set; } = new List<int>();
        public List<Person> SelectedActors { get; set; } = new List<Person>();
        public List<Person> SelectedDirectors { get; set; } = new List<Person>();
        public List<Keyword> SelectedKeywords { get; set; } = new List<Keyword>();

        protected override async Task OnInitializedAsync()
        {
            var results = await FetchMovieResults();

            if (results is not null)
            {
                Page = results.Page;
                Results = results.Results;
                TotalPages = results.Total_Pages;
                TotalResults = results.Total_Results;
            }
        }

        protected async Task HandleButtonClick()
        {
            Page = 1;

            var results = await FetchMovieResults();

            SearchActorComponent.ClearActor();
            SearchDirectorComponent.ClearDirector();
            SearchKeywordComponent.ClearKeyword();

            if (results is not null)
            {
                Page = results.Page;
                Results = results.Results;
                TotalPages = results.Total_Pages;
                TotalResults = results.Total_Results;
            }
        }

        protected void RemoveSelectedActor(Person actor)
        {
            SearchActorComponent.ClearActor();
            SelectedActors.Remove(actor);
        }
        protected void RemoveSelectedDirector(Person director)
        {
            SearchDirectorComponent.ClearDirector();
            SelectedDirectors.Remove(director);
        }

        protected void RemoveSelectedKeyword(Keyword keyword)
        {
            SearchKeywordComponent.ClearKeyword();
            SelectedKeywords.Remove(keyword);
        }

        protected void HandleDateSelection(DateSelectResult result)
        {
            if (result is not null)
            {
                if (result.Type == "from")
                    FromReleasedate = result.Value;

                if (result.Type == "to")
                    ToReleaseDate = result.Value;
            }
        }

        protected void HandleGenreSelection(GenreSelectResult result)
        {
            if (result is not null)
            {
                if (result.IsActive is true)
                {
                    SelectedGenreIds.Add(result.Id);
                }

                if (result.IsActive is not true)
                {
                    SelectedGenreIds.Remove(result.Id);
                }
            }
        }

        protected void HandleSortSelection(string selectedSortOrder) => SortOrder = selectedSortOrder;
        protected void HandleActorSelection(Person selectedActor) => SelectedActors.Add(selectedActor);
        protected void HandleDirectorSelection(Person selectedDirector) => SelectedDirectors.Add(selectedDirector);
        protected void HandleKeywordSelection(Keyword selectedKeyword) => SelectedKeywords.Add(selectedKeyword);

        protected void HandleRuntimeChanged(string selectedRuntime)
        {
            if (int.TryParse(selectedRuntime, out int parsedRuntime))
            {
                Runtime = parsedRuntime;
            }
        }

        protected void HandleYearSelection(string selectedYear)
        {
            if (int.TryParse(selectedYear, out int parsedYear))
            {
                ReleaseYear = parsedYear;
            }
        }

        protected async Task HandlePageChanged(int selectedPage)
        {
            Page = selectedPage;

            var results = await FetchMovieResults();

            if (results is not null)
            {
                Page = results.Page;
                Results = results.Results;
                TotalPages = results.Total_Pages;
                TotalResults = results.Total_Results;
            }
        }

        private async Task<MovieResults> FetchMovieResults()
        {
            var parameters = new MovieParameters
            {
                Page = Page,
                SortOrder = SortOrder,
                ReleaseYear = ReleaseYear,
                Runtime = Runtime,
                FromReleaseDate = FromReleasedate,
                ToReleaseDate = ToReleaseDate
            };

            if (SelectedGenreIds.Count() is not 0)
            {
                parameters.GenreIds = SelectedGenreIds;
            }

            if (SelectedActors.Count() is not 0)
            {
                foreach (var actor in SelectedActors)
                {
                    parameters.ActorIds.Add(actor.Id);
                }
            }

            if (SelectedDirectors.Count() is not 0)
            {
                foreach (var director in SelectedDirectors)
                {
                    parameters.DirectorIds.Add(director.Id);
                }
            }

            if (SelectedKeywords.Count() is not 0)
            {
                foreach (var keyword in SelectedKeywords)
                {
                    parameters.KeywordIds.Add(keyword.Id);
                }
            }

            var results = await DiscoverService.GetMoviesAsync(parameters);

            return results;
        }
    }
}
