using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorFrontEnd;
using BlazorFrontEnd.Components.Weather;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

var weatherHttpClient = new HttpClient { BaseAddress = new Uri("https://api.weatherapi.com") };
builder.Services.AddSingleton(new WeatherApiClient(weatherHttpClient));


await builder.Build().RunAsync();