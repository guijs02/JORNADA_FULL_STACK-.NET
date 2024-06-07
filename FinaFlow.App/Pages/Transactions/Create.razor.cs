using FinaFlow.Shared.Requests.Categories;
using FinaFlow.Shared.Requests.Transactions;
using FinaFlow.Shared.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FinaFlow.App.Pages.Transactions
{
    public class CreateTransactionsPage : ComponentBase
    {
        #region Properties
        public bool IsBusy { get; set; } = false;
        public CreateTransactionRequest InputModel { get; set; } = new();
        #endregion

        #region Services
        [Inject]
        public ITransactionService _service { get; set; } = null!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;

        #endregion

        #region Functions
        public async Task OnValidSubmitAsync()
        {
            IsBusy = true;

            try
            {
                var result = await _service.CreateAsync(InputModel);
                if (result.IsSuccess)
                {
                    Snackbar.Add(result.Message, Severity.Success);
                    NavigationManager.NavigateTo("/transactions");
                }
                else
                {
                    Snackbar.Add(result.Message, Severity.Error);
                }
            }
            catch (Exception e)
            {

                Snackbar.Add(e.Message, Severity.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion
    }
}

