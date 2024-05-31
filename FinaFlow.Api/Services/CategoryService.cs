using FinaFlow.Api.Data.Context;
using FinaFlow.Shared.Models;
using FinaFlow.Shared.Requests.Categories;
using FinaFlow.Shared.Responses;
using FinaFlow.Shared.Services;
using Microsoft.EntityFrameworkCore;

namespace FinaFlow.Api.Services
{
    public class CategoryService(AppDbContext context) : ICategoryService
    {
        public async Task<Response<Category?>> CreateAsync(CreateCategoryRequest request)
        {
            try
            {
                var category = new Category()
                {
                    UserId = request.UserId,
                    Title = request.Title,
                    Description = request.Description,
                };

                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();

                return new Response<Category?>(null, 201, "Categoria criada com sucesso!");

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request)
        {
            try
            {
                var category = await context
                                    .Categories
                                    .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (category == null)
                    return new Response<Category?>(null, 404, "Categoria não foi encontrada");


                context.Categories.Remove(category);
                await context.SaveChangesAsync();

                return new Response<Category?>(category, message:"Categoria Deletada com sucesso!");
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<PagedResponse<List<Category>>> GetAllAsync(GetAllCategoriesRequest request)
        {

            try
            {
                var query = context
                             .Categories
                             .AsNoTracking()
                             .Where(x => x.UserId == request.UserId)
                             .OrderBy(x => x.Title);

                var categories = await query
                        .Skip((request.PageNumber - 1) * request.PageSize)
                        .Take(request.PageSize)
                        .ToListAsync();

                var count = await query.CountAsync();


                return new PagedResponse<List<Category>?>(
                     categories, 
                     count,
                     request.PageNumber,
                     request.PageSize);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<Response<Category?>> GetByIdAsync(GetByIdCategoryRequest request)
        {
            try
            {
                var category = await context
                                    .Categories
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);


                return category is null 
                    ? new Response<Category?>(null,404, message: "Categoria recuperada com sucesso!")
                    : new Response<Category?>(category);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request)
        {
            try
            {
                var category = await context
                                    .Categories
                                    .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (category == null)
                    return new Response<Category?>(null, 404, "Categoria não foi encontrada");


                category.Title = request.Title;
                category.Description = request.Description;

                context.Categories.Update(category);
                await context.SaveChangesAsync();

                return new Response<Category?>(category, message:"Categoria atualizada com sucesso!");
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
