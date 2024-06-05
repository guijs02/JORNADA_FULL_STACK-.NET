using FinaFlow.Api;
using FinaFlow.Api.Common.Api;
using FinaFlow.Api.Data.Context;
using FinaFlow.Api.Endpoints;
using FinaFlow.Api.Services;
using FinaFlow.Shared.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfiguration();
builder.AddDataContexts();
builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddServices();
builder.Services.AddAuthorization();

var app = builder.Build();

if(app.Environment.IsDevelopment())
    app.ConfigureDevEnvironment();

app.UseHttpsRedirection();

app.UseCors(ApiConfiguration.CorsPolicyName);

app.MapEndpoints();
app.UseAuthorization();

app.Run();
