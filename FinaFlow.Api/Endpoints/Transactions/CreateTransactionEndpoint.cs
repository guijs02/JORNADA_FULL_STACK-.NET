using FinaFlow.Api.Common;
using FinaFlow.Shared.Models;
using FinaFlow.Shared.Requests.Categories;
using FinaFlow.Shared.Requests.Transactions;
using FinaFlow.Shared.Responses;
using FinaFlow.Shared.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinaFlow.Api.Endpoints.Transactions
{
    public class CreateTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapPost("/", HandleAsync)
                    .WithName("Transactions: Create")
                    .WithSummary("Cria transação")
                    .WithDescription("Cria transação")
                    .Produces<Response<Transaction?>>();

        }
        private static async Task<IResult> HandleAsync(
                [FromServices] ITransactionService service,
                [FromBody] CreateTransactionRequest request)
        {
            request.UserId = ApiConfiguration.UserId;

            var response = await service.CreateAsync(request);

            return response.IsSuccess
                    ? TypedResults.Created($"v1/transactions/{response.Data?.Id}", response)
                    : TypedResults.BadRequest(response);
        }
    }
}
