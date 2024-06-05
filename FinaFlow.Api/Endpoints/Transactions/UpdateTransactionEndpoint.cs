using FinaFlow.Api.Common;
using FinaFlow.Shared.Models;
using FinaFlow.Shared.Requests.Transactions;
using FinaFlow.Shared.Responses;
using FinaFlow.Shared.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinaFlow.Api.Endpoints.Transactions
{
    public class UpdateTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapPut("/{id}", HandleAsync)
                    .WithName("Transactions: Update")
                    .WithSummary("Altera a transação")
                    .WithDescription("Altera a transação")
                    .Produces<Response<Transaction>?>();

        }
        private static async Task<IResult> HandleAsync(
                [FromServices] ITransactionService service,
                [FromBody] UpdateTransactionRequest request,
                [FromRoute] long id)

        {
            request.UserId = ApiConfiguration.UserId ;

            var response = await service.UpdateAsync(request);

            return response.IsSuccess
                    ? TypedResults.Ok(response)
                    : TypedResults.BadRequest(response);
        }
    }
}
