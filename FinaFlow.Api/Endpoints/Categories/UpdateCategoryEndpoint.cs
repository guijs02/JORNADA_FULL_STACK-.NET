using FinaFlow.Api.Common;
using FinaFlow.Shared.Models;
using FinaFlow.Shared.Requests.Categories;
using FinaFlow.Shared.Responses;
using FinaFlow.Shared.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinaFlow.Api.Endpoints.Categories
{
    public class UpdateCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapPut("/{id}", HandleAsync)
                    .WithName("Categories: Update")
                    .WithSummary("Altera categoria")
                    .WithDescription("Altera categoria")
                    .Produces<Response<Category?>>();

        }
        private static async Task<IResult> HandleAsync(
                [FromServices] ICategoryService service,
                [FromBody] UpdateCategoryRequest request,
                [FromRoute] long id)

        {

            request.UserId = ApiConfiguration.UserId;
            request.Id = id;

            var response = await service.UpdateAsync(request);

            return response.IsSuccess
                    ? TypedResults.Ok(response)
                    : TypedResults.BadRequest(response);
        }
    }
}
