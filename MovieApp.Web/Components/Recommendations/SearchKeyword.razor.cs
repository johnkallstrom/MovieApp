﻿using Microsoft.AspNetCore.Components;
using MovieApp.Web.Models;
using MovieApp.Web.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.Recommendations
{
    public partial class SearchKeyword
    {
        [Inject]
        public ISearchHttpService SearchService { get; set; }

        public Keyword SelectedKeyword { get; set; }

        [Parameter]
        public EventCallback<Keyword> OnKeywordSelected { get; set; }

        public void ClearKeyword() => SelectedKeyword = null;

        private async Task<IEnumerable<Keyword>> SearchKeywords(string searchText)
        {
            var data = await SearchService.GetKeywordSearchAsync(searchText);

            return data.Results;
        }

        private async Task HandleKeywordSelection(Keyword result)
        {
            ClearKeyword();
            SelectedKeyword = result;
            await OnKeywordSelected.InvokeAsync(SelectedKeyword);
        }
    }
}
