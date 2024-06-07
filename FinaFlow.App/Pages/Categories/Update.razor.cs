using FinaFlow.Shared.Requests.Categories;
using FinaFlow.Shared.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FinaFlow.App.Pages.Categories
{
    public class UpdateCategoriesPage : ComponentBase
    {
        #region Properties
        public bool IsBusy { get; set; } = false;
        public UpdateCategoryRequest InputModel { get; set; } = new();
        #endregion
        #region Parameters
        [Parameter]
        public int id { get; set; }
        #endregion
        #region Services
        [Inject]
        public ICategoryService _service { get; set; } = null!;

        [Inject]
        public IDialogService Dialog { get; set; } = null!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;

        #endregion

        #region Functions
        protected override async Task OnInitializedAsync()
        {
            IsBusy = true;

            try
            {
                var request = new GetByIdCategoryRequest() { Id = id };

                var result = await _service.GetByIdAsync(request);

                if (result.IsSuccess)
                {
                    InputModel.Title = result.Data.Title;
                    InputModel.Description = result.Data.Description;
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

        public async Task OnValidSubmitAsync()
        {
            IsBusy = true;

            try
            {
                var result = await _service.UpdateAsync(InputModel);
                if (result.IsSuccess)
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

