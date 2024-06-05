using FinaFlow.Api.Common;
using FinaFlow.Shared.Models;
using FinaFlow.Shared.Requests.Categories;
using FinaFlow.Shared.Requests.Transactions;
using FinaFlow.Shared.Responses;
using FinaFlow.Shared.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinaFlow.Api.Endpoints.Transactions
{
    public class DeleteTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapDelete("/{id}", HandleAsync)
                    .WithName("Transactions: Delete")
                    .WithSummary("Deleta transação")
                    .WithDescription("Deleta transação")
                    .Produces<Response<Transaction?>>();

        }
        private static async Task<IResult> HandleAsync(
                [FromServices] ITransactionService service,
                [FromRoute] long id)

        {
            var request = new DeleteTransactionRequest
            {
                Id = id,
                UserId = ApiConfiguration.UserId,
            };

            var response = await service.DeleteAsync(request);

            return response.IsSuccess
                    ? TypedResults.Ok(response)
                    : TypedResults.BadRequest(response);
        }
    }
}
