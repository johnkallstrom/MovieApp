using Microsoft.AspNetCore.Components;
using MovieApp.Web.Services;
using System.Collections.Generic;
using System.Linq;

namespace MovieApp.Web.Components.Recommendations
{
    public partial class YearSelect
    {
        [Inject]
        public IDiscoverHttpService DiscoverService { get; set; }

        [Parameter]
        public EventCallback<string> OnYearSelection { get; set; }

        public IEnumerable<int> YearOptions { get; set; } = new List<int>();

        [Parameter]
        public int StartYear { get; set; }

        [Parameter]
        public int YearCount { get; set; }

        protected override void OnInitialized()
        {
            YearOptions = Enumerable.Range(StartYear, YearCount).OrderByDescending(year => year);
        }
    }
}
