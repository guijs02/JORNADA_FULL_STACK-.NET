
using FinaFlow.Api.Common;
using FinaFlow.Shared.Models;
using FinaFlow.Shared.Requests.Categories;
using FinaFlow.Shared.Responses;
using FinaFlow.Shared.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinaFlow.Api.Endpoints.Categories
{
    public class CreateCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
                app.MapPost("/", HandleAsync)
                        .WithName("Categories: Create")
                        .WithSummary("Cria categoria")
                        .WithDescription("Cria categoria")
                        .Produces<Response<Category?>>();

        }
        private static async Task<IResult> HandleAsync([FromServices] ICategoryService service, [FromBody] CreateCategoryRequest request)
        {
            request.UserId = ApiConfiguration.UserId;

            var response = await service.CreateAsync(request);

            return response.IsSuccess
                    ? TypedResults.Created($"v1/categories/{response.Data?.Id}", response)
                    : TypedResults.BadRequest(response);
        }
    }
}
