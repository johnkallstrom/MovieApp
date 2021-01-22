using System;

namespace MovieApp.Web.State
{
    public class SearchState
    {
        public string Query { get; set; }

        public event Action OnChange;

        public void SetSearchTerm(string value)
        {
            Query = value;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}