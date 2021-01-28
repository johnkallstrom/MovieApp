using System;
using System.Threading;

namespace MovieApp.Web.State
{
    public class SearchState
    {
        private Timer _timer;

        public string SearchQuery { get; set; }

        public event Action OnChange;

        public void SetSearchQuery(string value)
        {
            SearchQuery = value;
            NotifyStateChanged(500);
        }

        public void ClearSearchQuery()
        {
            SearchQuery = string.Empty;
            NotifyStateChanged(0);
        }

        private void NotifyStateChanged(int dueTime)
        {
            if (_timer != null)
                _timer.Dispose();

            _timer = new Timer(OnTimerElapsed, null, dueTime, 0);
        }

        private void OnTimerElapsed(object state)
        {
            OnChange?.Invoke();
            _timer.Dispose();
        }
    }
}
