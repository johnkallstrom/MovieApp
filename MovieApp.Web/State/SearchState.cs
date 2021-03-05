using MovieApp.Web.Enums;
using MovieApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Threading;

namespace MovieApp.Web.State
{
    public class SearchState
    {
        private Timer _timer;

        public int Page { get; set; } = 1;
        public SearchFilterType Filter { get; set; }
        public int TotalPages { get; set; }
        public int TotalResults { get; set; }
        public string Query { get; set; }
        public IEnumerable<Media> Results { get; set; } = new List<Media>();

        #region Events
        public event Action OnPageChange;
        public event Action OnPageReset;
        public event Action OnFilterChange;
        public event Action OnFilterReset;
        public event Action OnTotalPagesChange;
        public event Action OnTotalResultsChange;
        public event Action OnResultsChange;
        public event Action OnQueryChange;
        public event Action OnQueryClear;
        #endregion

        #region Public Methods
        public void SetPage(int page)
        {
            Page = page;
            OnPageChange?.Invoke();
        }

        public void ResetPage()
        {
            Page = 1;
            OnPageReset?.Invoke();
        }

        public void SetFilter(SearchFilterType filterType)
        {
            Filter = filterType;
            OnFilterChange?.Invoke();
        }

        public void ResetFilter()
        {
            Filter = SearchFilterType.All;
            OnFilterReset?.Invoke();
        }

        public void SetTotalPages(int totalPages)
        {
            TotalPages = totalPages;
            OnTotalPagesChange?.Invoke();
        }

        public void SetTotalResults(int totalResults)
        {
            TotalResults = totalResults;
            OnTotalResultsChange?.Invoke();
        }

        public void SetResults(IEnumerable<Media> results)
        {
            Results = results;
            OnResultsChange?.Invoke();
        }

        public void Clear()
        {
            ClearQuery();
            ResetFilter();
        }

        public void ClearQuery()
        {
            Query = string.Empty;
            OnQueryClear?.Invoke();
        }

        public void SetQuery(string value)
        {
            if (_timer != null)
                _timer.Dispose();

            Query = value;

            _timer = new Timer(OnElapsedTimer, null, 500, 0);
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
