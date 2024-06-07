using FinaFlow.Shared.Models;
using FinaFlow.Shared.Requests.Categories;
using FinaFlow.Shared.Responses;
using FinaFlow.Shared.Services;
using System.Net.Http.Json;

namespace FinaFlow.App.Services
{
    public class CategoryService(IHttpClientFactory httpClient) : ICategoryService
    {
        public readonly HttpClient _client = httpClient.CreateClient();
        public async Task<Response<Category?>> CreateAsync(CreateCategoryRequest request)
        {
            var result = await _client.PostAsJsonAsync("v1/categories", request);
            return await result.Content.ReadFromJsonAsync<Response<Category?>>()
                ?? new Response<Category?>(null, 400, "Falha ao criar");
        }

        public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request)
        {
            var result = await _client.DeleteAsync($"v1/categories/{request.Id}");
            return await result.Content.ReadFromJsonAsync<Response<Category?>>()
                  ?? new Response<Category?>(null, 400, "Falha ao deletar");
        }

        public async Task<PagedResponse<List<Category>>> GetAllAsync(GetAllCategoriesRequest request)
        {
            var result = await _client.GetAsync("v1/categories");
            return await result.Content.ReadFromJsonAsync<PagedResponse<List<Category>?>>()
                ?? new PagedResponse<List<Category>>(null, 400, "Falha ao obter as categorias");
        }

        public async Task<Response<Category?>> GetByIdAsync(GetByIdCategoryRequest request)
        {
            var result = await _client.GetAsync($"v1/categories/{request.Id}");
            return await result.Content.ReadFromJsonAsync<Response<Category?>>()
                         ?? new Response<Category?>(null, 400, "Falha ao obter categoria");
        }

        public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request)
        {
            var result = await _client.PutAsJsonAsync($"v1/categories/{request.Id}", request);
            return await result.Content.ReadFromJsonAsync<Response<Category?>>()
                         ?? new Response<Category?>(null, 400, "Falha ao alterar"); ;
        }
    }
}
