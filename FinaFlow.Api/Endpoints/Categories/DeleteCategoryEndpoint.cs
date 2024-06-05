using FinaFlow.Api.Common;
using FinaFlow.Shared.Models;
using FinaFlow.Shared.Requests.Categories;
using FinaFlow.Shared.Responses;
using FinaFlow.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinaFlow.Api.Endpoints.Categories
{
    public class DeleteCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapDelete("/{id}", HandleAsync)
                    .WithName("Categories: Delete")
                    .WithSummary("Deleta categoria")
                    .WithDescription("Deleta categoria")
                    .Produces<Response<Category?>>();

        }
        private static async Task<IResult> HandleAsync(
                [FromServices] ICategoryService service,
                [FromRoute] long id)

        {
            var deleteRequest = new DeleteCategoryRequest
            {
                Id = id,
                UserId = ApiConfiguration.UserId,
            };

            var response = await service.DeleteAsync(deleteRequest);

            return response.IsSuccess
                    ? TypedResults.Ok(response)
                    : TypedResults.BadRequest(response);
        }
    }
}
