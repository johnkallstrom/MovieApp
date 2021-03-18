using Blazored.LocalStorage;
using Blazored.Modal;
using Blazored.Toast;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MovieApp.Web.Clients;
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

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["API:BaseUrl"]) });

            builder.Services.AddTransient<IAuthenticationHttpService, AuthenticationHttpService>();
            builder.Services.AddTransient<IUserHttpService, UserHttpService>();
            builder.Services.AddTransient<IListHttpService, ListHttpService>();

            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

            builder.Services.AddHttpClient<ITMDBClient, TMDBClient>(client => client.BaseAddress = new Uri(builder.Configuration["TMDB:BaseUrl"]));

            builder.Services.AddTransient<IMovieHttpService, MovieHttpService>();
            builder.Services.AddTransient<IPeopleHttpService, PeopleHttpService>();
            builder.Services.AddTransient<ITVHttpService, TVHttpService>();
            builder.Services.AddTransient<ISearchHttpService, SearchHttpService>();
            builder.Services.AddTransient<ITrendingHttpService, TrendingHttpService>();
            builder.Services.AddTransient<IDiscoverHttpService, DiscoverHttpService>();
            builder.Services.AddTransient<IGenreHttpService, GenreHttpService>();

            builder.Services.AddSingleton<SearchState>();

            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddBlazoredModal();
            builder.Services.AddBlazoredToast();

            await builder.Build().RunAsync();
        }
    }
}
