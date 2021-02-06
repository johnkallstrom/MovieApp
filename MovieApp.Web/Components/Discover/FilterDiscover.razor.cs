using Microsoft.AspNetCore.Components;
using MovieApp.Web.Enums;
using MovieApp.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.Discover
{
    public partial class FilterDiscover
    {
        public IEnumerable<SelectOption> MediaTypeOptions { get; set; } = new List<SelectOption>
        {
            new SelectOption("Movies", MediaType.Movie),
            new SelectOption("TV Shows", MediaType.TV),
        };

        public string SelectedMediaType { get; set; } = MediaType.Movie;

        [Parameter]
        public EventCallback<string> OnMediaSelection { get; set; }

        protected async Task HandleSelect(string value)
        {
            SelectedMediaType = value;
            await OnMediaSelection.InvokeAsync(SelectedMediaType);
        }
    }
}
