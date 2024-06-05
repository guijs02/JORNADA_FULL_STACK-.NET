using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using FinaFlow.App;
using MudBlazor.Services;
using FinaFlow.Shared;
using FinaFlow.Shared.Services;
using FinaFlow.App.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();
builder.Services.AddHttpClient(
    WebConfiguration.HttpClientName,
    opts =>
    {
        opts.BaseAddress = new Uri(Configuration.BackendUrl);
    });
    Console.WriteLine(Configuration.BackendUrl);

builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();


await builder.Build().RunAsync();
