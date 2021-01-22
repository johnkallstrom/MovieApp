using Microsoft.AspNetCore.Components;
using MovieApp.Web.Models;

namespace MovieApp.Web.Components.People
{
    public partial class DisplayPerson
    {
        [Parameter]
        public Person Person { get; set; }

        [Parameter]
        public bool ShowCharacter { get; set; }
    }
}
