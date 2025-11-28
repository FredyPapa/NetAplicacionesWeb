using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ECommerceWeb.WebApp;
using CurrieTechnologies.Razor.SweetAlert2;
using Blazored.Toast;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using ECommerceWeb.WebApp.Auth;
using Blazored.LocalStorage;
using ECommerceWeb.WebApp.Proxy.Interfaces;
using ECommerceWeb.WebApp.Proxy.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddBlazoredToast();
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddSweetAlert2();

builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationService>();
builder.Services.AddScoped<ICarritoProxy, CarritoProxy>();
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
