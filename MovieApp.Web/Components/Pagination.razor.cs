using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace MovieApp.Web.Components
{
    public partial class Pagination
    {
        [Parameter]
        public int CurrentPage { get; set; }

        [Parameter]
        public int TotalPages { get; set; }

        [Parameter]
        public EventCallback<int> OnCurrentPageChanged { get; set; }

        protected void HandlePrevBtn()
        {
            System.Console.WriteLine($"PrevBtn Clicked.");
        }

        protected void HandleNextBtn()
        {
            System.Console.WriteLine($"NextBtn Clicked.");
        }
    }
}
