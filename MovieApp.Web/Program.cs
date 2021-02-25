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

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["TMDB:ApiBaseUrl"]) });

            builder.Services.AddTransient<IMovieService, MovieService>();
            builder.Services.AddTransient<IPeopleService, PeopleService>();
            builder.Services.AddTransient<ITVService, TVService>();
            builder.Services.AddTransient<ISearchService, SearchService>();
            builder.Services.AddTransient<ITrendingService, TrendingService>();
            builder.Services.AddTransient<IDiscoverService, DiscoverService>();
            builder.Services.AddTransient<IGenreService, GenreService>();

            builder.Services.AddSingleton<SearchState>();

            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddHttpClient<IUserService, UserService>(client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["API:BaseUrl"]);
            });

            await builder.Build().RunAsync();
        }
    }
}
