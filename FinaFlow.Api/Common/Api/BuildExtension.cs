using FinaFlow.Api.Data.Context;
using FinaFlow.Api.Services;
using FinaFlow.Shared;
using FinaFlow.Shared.Services;
using Microsoft.EntityFrameworkCore;

namespace FinaFlow.Api.Common.Api
{
    public static class BuildExtension
    {
        public static void AddConfiguration(this WebApplicationBuilder builder)
        {
            ApiConfiguration.ConnectionString = builder.Configuration.GetConnectionString("Connection") ?? string.Empty;
            Configuration.BackendUrl = builder.Configuration["Url:Backend"] ?? string.Empty;
            Configuration.FrontendUrl = builder.Configuration["Url:Frontend"] ?? string.Empty;

        }
        public static void AddDocumentation(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(s =>
            {
                s.CustomSchemaIds(n => n.FullName);
            });
        }
        public static void AddDataContexts(this WebApplicationBuilder builder)
        {
            builder
                .Services
                .AddDbContext<AppDbContext>(
                 x =>
                 {
                     x.UseSqlServer(builder.Configuration.GetConnectionString("Connection"));
                 });
        }
        public static void AddCrossOrigin(this WebApplicationBuilder builder)
        {
            builder.Services.AddCors(
                options => options.AddPolicy(
                    ApiConfiguration.CorsPolicyName,
                        policy => policy.WithOrigins([
                            Configuration.BackendUrl,
                            Configuration.FrontendUrl])
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    ));

        }    
        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<ICategoryService, CategoryService>();
            builder.Services.AddTransient<ITransactionService, TransactionService>();

        }
    }
}
