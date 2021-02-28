using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MovieApp.Web.State;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MovieApp.Web.Shared
{
    public partial class Navbar
    {
        [Inject]
        public SearchState SearchState { get; set; }
    }
}