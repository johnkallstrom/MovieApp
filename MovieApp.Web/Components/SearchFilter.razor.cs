using Microsoft.AspNetCore.Components;
using MovieApp.Web.Enums;
using MovieApp.Web.State;
using System;

namespace MovieApp.Web.Components
{
    public partial class SearchFilter
    {
        [Inject]
        public SearchState SearchState { get; set; }

        protected void HandleFilterChange(SearchFilterType type)
        {
            Console.WriteLine(type.ToString());
        }
    }
}
