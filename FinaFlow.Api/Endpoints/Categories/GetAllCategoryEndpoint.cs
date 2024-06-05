using FinaFlow.Api.Common;
using FinaFlow.Shared;
using FinaFlow.Shared.Models;
using FinaFlow.Shared.Requests.Categories;
using FinaFlow.Shared.Responses;
using FinaFlow.Shared.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinaFlow.Api.Endpoints.Categories
{
    public class GetAllCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/", HandleAsync)
                    .WithName("Categories: Get All")
                    .WithSummary("Recupera todas as categorias")
                    .WithDescription("Recupera todas as categorias")
                    .Produces<PagedResponse<List<Category>?>>();

        }
        private static async Task<IResult> HandleAsync(
                [FromServices] ICategoryService service,
                [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
                [FromQuery] int pageSize = Configuration.DefaultPageSize)

        {
            var request = new GetAllCategoriesRequest
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                UserId = ApiConfiguration.UserId,
            };

            var response = await service.GetAllAsync(request);

            return response.IsSuccess
                    ? TypedResults.Ok(response)
                    : TypedResults.BadRequest(response);
        }
    }
}
