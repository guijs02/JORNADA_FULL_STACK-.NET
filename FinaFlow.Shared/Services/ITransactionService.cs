using FinaFlow.Shared.Models;
using FinaFlow.Shared.Requests.Categories;
using FinaFlow.Shared.Requests.Transactions;
using FinaFlow.Shared.Responses;

namespace FinaFlow.Shared.Services
{
    public interface ITransactionService
    {
        Task<Response<Category?>> CreateAsync(CreateTransactionRequest request);
        Task<Response<Category?>> UpdateAsync(UpdateCategoyRequest request);
        Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request);
        Task<Response<Category?>> GetByIdAsync(GetByIdCategoryRequest request);
        Task<PagedResponse<List<Category>>> GetAllAsync(GetAllCategoriesRequest request);
    }
}
