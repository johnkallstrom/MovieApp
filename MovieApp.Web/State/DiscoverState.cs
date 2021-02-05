using MovieApp.Web.Models;
using System;
using System.Collections.Generic;

namespace MovieApp.Web.State
{
    public class DiscoverState
    {
        #region Properties
        public int Page { get; set; } = 1;
        public IEnumerable<Movie> MovieResults { get; set; } = new List<Movie>();
        public IEnumerable<TVShow> TVResults { get; set; } = new List<TVShow>();
        public int TotalPages { get; set; }
        public int TotalResults { get; set; }
        #endregion

        #region Events
        public event Action OnPageChange;
        public event Action OnMovieResultsChange;
        public event Action OnTVResultsChange;
        public event Action OnTotalPagesChange;
        public event Action OnTotalResultsChange;
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
            OnPageChange?.Invoke();
        }

        public void SetMovieResults(IEnumerable<Movie> results)
        {
            MovieResults = results;
            OnMovieResultsChange?.Invoke();
        }

        public void SetTVResults(IEnumerable<TVShow> results)
        {
            TVResults = results;
            OnMovieResultsChange?.Invoke();
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
        #endregion
    }
}
