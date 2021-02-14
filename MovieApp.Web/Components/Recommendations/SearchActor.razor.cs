using Microsoft.AspNetCore.Components;
using MovieApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.Recommendations
{
    public partial class SearchActor
    {
        private Timer _timer;

        public string SearchQuery { get; set; }

        public IEnumerable<Person> Results { get; set; } = new List<Person>();

        [Parameter]
        public EventCallback<string> OnQueryChange { get; set; }

        public string Placeholder { get; set; } = "Search actor...";

        public void SetQuery(string value)
        {
            if (_timer != null)
            {
                _timer.Dispose();
            }

            SearchQuery = value;

            _timer = new Timer(OnElapsedTimer, null, 500, 0);
        }

        private void OnElapsedTimer(object state)
        {
            OnQueryChange.InvokeAsync(SearchQuery);
            _timer.Dispose();
        }
    }
}
