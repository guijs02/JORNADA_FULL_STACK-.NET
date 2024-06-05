using FinaFlow.Api.Common;
using FinaFlow.Shared.Models;
using FinaFlow.Shared.Requests.Categories;
using FinaFlow.Shared.Responses;
using FinaFlow.Shared.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinaFlow.Api.Endpoints.Categories
{
    public class GetByIdCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/{id}", HandleAsync)
                    .WithName("Categories Get By Id")
                    .WithSummary("Obter categoria")
                    .WithDescription("Obter categoria")
                    .Produces<Response<Category>?>();

        }
        private static async Task<IResult> HandleAsync(
                [FromServices] ICategoryService service,
                [FromRoute] long id)

        {
            var request = new GetByIdCategoryRequest
            {
                Id = id,
                UserId = ApiConfiguration.UserId,
            };

            var response = await service.GetByIdAsync(request);

            return response.IsSuccess
                    ? TypedResults.Ok(response)
                    : TypedResults.BadRequest(response);
        }
    }
}
