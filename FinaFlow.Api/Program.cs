using FinaFlow.Api.Data.Context;
using FinaFlow.Api.Services;
using FinaFlow.Shared.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//builder.Services.AddDbContext<AppDbContext>(
//        x => x.UseSqlServer(connectionString)
//);

builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<ITransactionService, TransactionService>();
//builder.Services.AddScoped<DbContext, AppDbContext>();

app.MapGet("/", () => "Hello World!");

app.Run();
