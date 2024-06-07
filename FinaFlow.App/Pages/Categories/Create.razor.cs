using FinaFlow.Shared.Requests.Categories;
using FinaFlow.Shared.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FinaFlow.App.Pages.Categories
{
    public partial class CreateCategoryPage : ComponentBase
    {
        #region Properties
        public bool IsBusy { get; set; } = false;
        public CreateCategoryRequest InputModel { get; set; } = new();
        #endregion

        #region Services
        [Inject]
        public ICategoryService _service { get; set; } = null!;

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
                if(result.IsSuccess)
                {
                    Snackbar.Add(result.Message, Severity.Success);
                    NavigationManager.NavigateTo("/categories");
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
