using Microsoft.AspNetCore.Components;
using MovieApp.Web.Services;
using MovieApp.Web.State;
using System.Threading.Tasks;

namespace MovieApp.Web.Components
{
    public partial class SearchFilter
    {
        [Inject]
        public SearchState SearchState { get; set; }
    }
}
