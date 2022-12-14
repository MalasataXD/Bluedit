using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlueditBlazor;
using BlueditBlazor.Auth;
using BlueditBlazor.Services.Http;
using HttpClients.Interfaces;
using HttpClients.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Shared.Auth;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");




// ¤ Added by ME
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7157") });
builder.Services.AddScoped<IUserService, UserHttpClient>();
builder.Services.AddScoped<IPostService, PostHttpClient>();

// ! Shared
builder.Services.AddScoped<IAuthService, JwtAuthService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthProvider>();
AuthorizationPolicies.AddPolicies(builder.Services);

await builder.Build().RunAsync();