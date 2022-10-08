using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlueditWASM;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// ¤ Added by ME
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7157") });




await builder.Build().RunAsync();