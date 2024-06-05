using FinaFlow.Api.Common;
using FinaFlow.Api.Endpoints.Categories;
using FinaFlow.Api.Endpoints.Transactions;

namespace FinaFlow.Api.Endpoints
{
    public static class Endpoint
    {
        public static void MapEndpoints(this WebApplication app)
        {
            var endpoints = app.MapGroup("");

            endpoints.MapGroup("/")
                 .WithTags("Health Check")
                 .MapGet("/", () => new { message = "OK" });

            endpoints.MapGroup("v1/categories")
                .WithTags("Categories")
                .MapEndpoint<GetAllCategoryEndpoint>()
                .MapEndpoint<GetByIdCategoryEndpoint>()
                .MapEndpoint<CreateCategoryEndpoint>()
                .MapEndpoint<DeleteCategoryEndpoint>()
                .MapEndpoint<UpdateCategoryEndpoint>();

            endpoints.MapGroup("v1/transactions")
                .WithTags("Transactions")
                .RequireAuthorization()
                .MapEndpoint<GetTransactionByPeriodEndpoint>()
                .MapEndpoint<GetTransactionByIdEndpoint>()
                .MapEndpoint<CreateTransactionEndpoint>()
                .MapEndpoint<UpdateTransactionEndpoint>()
                .MapEndpoint<DeleteTransactionEndpoint>();
        }
        private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder builder)
            where TEndpoint : IEndpoint
        {
            TEndpoint.Map(builder);
            return builder;
        }
    }

}
