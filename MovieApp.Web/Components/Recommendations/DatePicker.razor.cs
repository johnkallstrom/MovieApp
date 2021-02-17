using Microsoft.AspNetCore.Components;
using MovieApp.Web.Data;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.Recommendations
{
    public partial class DatePicker
    {
        [Parameter]
        public EventCallback<DateSelectResult> OnDateSelection { get; set; }

        protected async Task HandleDateSelection(ChangeEventArgs e, string type)
        {
            await OnDateSelection.InvokeAsync(new DateSelectResult { Value = e.Value.ToString(), Type = type });
        }
    }
}
