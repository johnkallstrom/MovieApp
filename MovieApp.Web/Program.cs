using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MovieApp.Web.Services;
using MovieApp.Web.State;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MovieApp.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://api.themoviedb.org/3/") });

            builder.Services.AddTransient<IMovieService, MovieService>();
            builder.Services.AddTransient<IPeopleService, PeopleService>();
            builder.Services.AddTransient<ITVService, TVService>();
            builder.Services.AddTransient<IConfigurationService, ConfigurationService>();
            builder.Services.AddTransient<ISearchService, SearchService>();

            builder.Services.AddSingleton<SearchState>();

            await builder.Build().RunAsync();
        }
    }
}
