using FinaFlow.Api.Data.Context;
using FinaFlow.Shared.Common;
using FinaFlow.Shared.Models;
using FinaFlow.Shared.Models.Enums;
using FinaFlow.Shared.Requests.Categories;
using FinaFlow.Shared.Requests.Transactions;
using FinaFlow.Shared.Responses;
using FinaFlow.Shared.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace FinaFlow.Api.Services
{
    public class TransactionService(AppDbContext context) : ITransactionService
    {
        public async Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request)
        {
            if (request is { Type: ETransactionType.Withdraw, Amount: >= 0 })
                request.Amount *= -1;
            try
            {
                var transaction = new Transaction
                {
                    CategoryId = request.CategoryId,
                    CreatedAt = DateTime.Now,
                    Amount = request.Amount,
                    PaidOrReceivedAt = request.PaidOrReceivedAt,
                    Title = request.Title,
                    Type = request.Type,
                };

                await context.Transactions.AddAsync(transaction);
                await context.SaveChangesAsync();

                return new Response<Transaction?>(transaction, 201, "Transação adicionada com sucesso!");
            }
            catch
            {
                return new Response<Transaction?>(null, 500, "Ocorreu um erro!");

                throw;
            }
        }

        public async Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
        {
            try
            {
                var transaction = await context
                       .Transactions
                       .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (transaction is null)
                    return new Response<Transaction?>(null, 404);

                context.Transactions.Remove(transaction);
                await context.SaveChangesAsync();

                return new Response<Transaction?>(transaction);
            }
            catch
            {
                return new Response<Transaction?>(null, 500, "Ocorreu um erro!");
            }
        }

        public async Task<PagedResponse<List<Transaction>>> GetByPeriodAsync(GetTransactionByPeriodRequest request)
        {
            try
            {
                request.StartDate ??= DateTime.Now.GetFirstDay();
                request.EndDate ??= DateTime.Now.GetLastDay();

                var query = context
                            .Transactions
                            .AsNoTracking()
                            .Where(x =>
                                x.PaidOrReceivedAt >= request.StartDate &&
                                x.PaidOrReceivedAt <= request.EndDate &&
                                x.UserId == request.UserId)
                            .OrderBy(x => x.PaidOrReceivedAt);

                var transactions = await query
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

                var count = await query.CountAsync();

                return new PagedResponse<List<Transaction>?>(
                    transactions,
                    count,
                    request.PageNumber,
                    request.PageSize);
            }
            catch
            {
                return new PagedResponse<List<Transaction>?>(null, 500, "Não foi possivel realizar a operação");
            }
        }

        public async Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request)
        {
            try
            {
                var transaction = await context
                                       .Transactions
                                       .AsNoTracking()
                                       .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);


                return transaction is null
                    ? new Response<Transaction?>(null, 404, message: "Transação recuperada com sucesso!")
                    : new Response<Transaction?>(transaction);
            }
            catch (Exception)
            {
                return new Response<Transaction?>(null, 500, "Ocorreu um erro!");
            }
        }

        public async Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
        {
            if (request is { Type: ETransactionType.Withdraw, Amount: >= 0 })
                request.Amount *= -1;
            try
            {
                var transaction = await context
                       .Transactions
                       .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (transaction is null)
                    return new Response<Transaction?>(null, 404);

                transaction.CategoryId = request.CategoryId;
                transaction.Amount = request.Amount;
                transaction.PaidOrReceivedAt = request.PaidOrReceivedAt;
                transaction.Title = request.Title;
                transaction.Type = request.Type;

                context.Transactions.Update(transaction);
                await context.SaveChangesAsync();

                return new Response<Transaction?>(transaction);
            }
            catch
            {
                return new Response<Transaction?>(null, 500, "Ocorreu um erro!");
            }
        }
    }
}
