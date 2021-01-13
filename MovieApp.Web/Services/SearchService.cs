using System;

namespace MovieApp.Web.Services
{
    public class SearchService
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

        public event Action OnQueryChange;
        private void NotifyDataChanged() => OnQueryChange?.Invoke();
    }
}