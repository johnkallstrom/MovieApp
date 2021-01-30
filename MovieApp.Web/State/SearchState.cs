using MovieApp.Web.Models;
using System;
using System.Threading;

namespace MovieApp.Web.State
{
    public class SearchState
    {
        private Timer _timer;

        public SearchResults Data { get; set; } = new SearchResults();
        public string Query { get; set; }
        public int Page { get; set; } = 1;

        public event Action OnPageChange;
        public event Action OnDataChange;
        public event Action OnQueryChange;
        public event Action OnQueryClear;

        #region Public Methods
        public void SetPage(int page)
        {
            Page = page;
            OnPageChange?.Invoke();
        }

        public void SetData(SearchResults data)
        {
            Data = data;
            OnDataChange?.Invoke();
        }

        public void SetQuery(string value)
        {
            if (_timer != null)
                _timer.Dispose();

            Query = value;

            _timer = new Timer(OnElapsedTimer, null, 500, 0);
        }

        public void ClearQuery()
        {
            Query = string.Empty;
            OnQueryClear?.Invoke();
        }
        #endregion

        #region Private Methods
        private void OnElapsedTimer(object state)
        {
            OnQueryChange?.Invoke();
            _timer.Dispose();
        }
        #endregion
    }
}
