using Blazored.LocalStorage;
using Mari.Client;
using Mari.Client.Common.Http;
using Mari.Client.Common.Mapping;
using Mari.Client.Common.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient();
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddMudServices();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddClientServices();
builder.Services.AddHttpUtils(builder.HostEnvironment);
builder.Services.AddMapping();

await builder.Build().RunAsync();
