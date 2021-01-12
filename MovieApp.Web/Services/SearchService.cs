using System;

namespace MovieApp.Web.Services
{
    public class SearchService : ISearchService
    {
        private string _query;
        public string Query
        {
            get
            {
                return _query;
            }
            set
            {
                _query = value;
                NotifyDataChanged();
            }
        }

        public event Action OnChange;
        private void NotifyDataChanged() => OnChange?.Invoke();
    }
}
