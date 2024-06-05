using FinaFlow.Api.Common;
using FinaFlow.Shared;
using FinaFlow.Shared.Models;
using FinaFlow.Shared.Requests.Categories;
using FinaFlow.Shared.Requests.Transactions;
using FinaFlow.Shared.Responses;
using FinaFlow.Shared.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinaFlow.Api.Endpoints.Transactions
{
    public class GetTransactionByPeriodEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/", HandleAsync)
                    .WithName("Transactions: Get All")
                    .WithSummary("Recupera todas as transações")
                    .WithDescription("Recupera todas as transações")
                    .Produces<PagedResponse<List<Transaction>?>>();

        }
        private static async Task<IResult> HandleAsync(
                [FromServices] ITransactionService service,
                [FromQuery] DateTime? startDate = null,
                [FromQuery] DateTime? endDate = null,
                [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
                [FromQuery] int pageSize = Configuration.DefaultPageSize)

        {
            var request = new GetTransactionByPeriodRequest
            {
                UserId = ApiConfiguration.UserId,
                PageNumber = pageNumber,
                PageSize = pageSize,
                StartDate = startDate,
                EndDate = endDate,
            };

            var response = await service.GetByPeriodAsync(request);

            return response.IsSuccess
                    ? TypedResults.Ok(response)
                    : TypedResults.BadRequest(response);
        }
    }
}
