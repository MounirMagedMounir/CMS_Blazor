using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazor_CMS.Client.Services;


var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Register the HttpClient
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7113"),  // Set to the API base address
});


// Register the ApiService
builder.Services.AddScoped<AuthenticationServices>(); // It will automatically inject IJSRuntime
builder.Services.AddScoped<NotificationService>();

await builder.Build().RunAsync();
