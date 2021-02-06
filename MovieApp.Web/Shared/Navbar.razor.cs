using Microsoft.AspNetCore.Components;
using MovieApp.Web.Models;
using MovieApp.Web.State;
using System.Collections.Generic;

namespace MovieApp.Web.Shared
{
    public partial class Navbar
    {
        [Inject]
        public SearchState SearchState { get; set; }
    }
}
