using MovieApp.Web.Models;
using System;
using System.Threading;

namespace MovieApp.Web.State
{
    public class SearchState
    {
        private Timer _timer;

        public SearchResults SearchResults { get; set; } = new SearchResults();
        public string SearchQuery { get; set; }
        public int CurrentPage { get; set; } = 1;

        public event Action OnCurrentPageChange;
        public event Action OnSearchResultsChange;
        public event Action OnSearchQueryChange;
        public event Action OnSearchQueryClear;

        #region Public Methods
        public void SetCurrentPage(int page)
        {
            CurrentPage = page;
            OnCurrentPageChange?.Invoke();
        }

        public void SetSearchResults(SearchResults results)
        {
            SearchResults = results;
            OnSearchResultsChange?.Invoke();
        }

        public void SetSearchQuery(string value)
        {
            if (_timer != null)
                _timer.Dispose();

            SearchQuery = value;

            _timer = new Timer(InvokeSearchQueryEvent, null, 500, 0);
        }

        public void ClearSearchQuery()
        {
            SearchQuery = string.Empty;
            OnSearchQueryClear?.Invoke();
        }
        #endregion

        #region Private Methods
        private void InvokeSearchQueryEvent(object state)
        {
            OnSearchQueryChange?.Invoke();
            _timer.Dispose();
        }
        #endregion
    }
}
