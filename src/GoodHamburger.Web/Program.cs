using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using GoodHamburger.Web;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp =>
{
    var baseAddress = builder.HostEnvironment.BaseAddress;

#if DEBUG
    baseAddress = "http://localhost:8080/";
#else
    baseAddress = "http://localhost:8080/";
#endif

    return new HttpClient { BaseAddress = new Uri(baseAddress) };
});


builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<OrderService>();

await builder.Build().RunAsync();
