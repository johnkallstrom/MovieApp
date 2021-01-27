using Microsoft.AspNetCore.Components;
using System.Threading;

namespace MovieApp.Web.Shared
{
    public partial class AnotherSearch
    {
        private Timer _timer;

        public string SearchQuery { get; set; }

        [Parameter]
        public EventCallback<string> OnSearchChanged { get; set; }

        private void SearchChanged()
        {
            if (_timer != null)
                _timer.Dispose();

            _timer = new Timer(OnTimerElapsed, null, 500, 0);
        }

        private void OnTimerElapsed(object state)
        {
            OnSearchChanged.InvokeAsync(SearchQuery);
            _timer.Dispose();
        }
    }
}
