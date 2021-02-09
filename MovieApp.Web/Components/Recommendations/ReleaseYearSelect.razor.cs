using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;

namespace MovieApp.Web.Components.Recommendations
{
    public partial class ReleaseYearSelect
    {
        [Parameter]
        public EventCallback<string> OnYearSelection { get; set; }

        public IEnumerable<int> ReleaseYearOptions { get; set; } = new List<int>();

        [Parameter]
        public int StartReleaseYear { get; set; }

        [Parameter]
        public int YearCount { get; set; }

        protected override void OnInitialized()
        {
            ReleaseYearOptions = Enumerable.Range(StartReleaseYear, YearCount).OrderByDescending(year => year);
        }
    }
}
