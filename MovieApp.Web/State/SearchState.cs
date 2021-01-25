using System;
using System.Threading;

namespace MovieApp.Web.State
{
    public class SearchState
    {
        private Timer _timer;

        public string Query { get; set; }

        public event Action OnChange;

        public void SetSearchTerm(string value)
        {
            if (_timer != null)
                _timer.Dispose();

            Query = value;

            _timer = new Timer(OnTimerElapsed, null, 500, 0);
        }

        private void OnTimerElapsed(object state)
        {
            OnChange?.Invoke();
            _timer.Dispose();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}