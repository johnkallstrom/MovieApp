using Microsoft.AspNetCore.Components;
using MovieApp.Web.Models;
using System.Collections.Generic;

namespace MovieApp.Web.Components
{
    public partial class SearchResults
    {
        [Parameter]
        public string Query { get; set; }

        [Parameter]
        public int TotalResults { get; set; }

        [Parameter]
        public IEnumerable<Media> Results { get; set; } = new List<Media>();

        protected override void OnInitialized()
        {
            foreach (var item in Results)
            {
                if (!string.IsNullOrWhiteSpace(item.Title))
                {
                    System.Console.WriteLine(item.Title);
                }


                if (!string.IsNullOrWhiteSpace(item.Name))
                {
                    System.Console.WriteLine(item.Name);
                }
            }
        }
    }
}
