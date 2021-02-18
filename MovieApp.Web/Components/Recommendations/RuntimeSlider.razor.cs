using Microsoft.AspNetCore.Components;

namespace MovieApp.Web.Components.Recommendations
{
    public partial class RuntimeSlider
    {
        [Parameter]
        public EventCallback<string> OnRuntimeChanged { get; set; }
    }
}
