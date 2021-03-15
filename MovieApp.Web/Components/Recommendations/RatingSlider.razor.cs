using Microsoft.AspNetCore.Components;

namespace MovieApp.Web.Components.Recommendations
{
    public partial class RatingSlider
    {
        [Parameter]
        public EventCallback<string> OnRatingChanged { get; set; }
    }
}
