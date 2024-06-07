using FinaFlow.Shared.Models;
using FinaFlow.Shared.Requests.Categories;
using FinaFlow.Shared.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FinaFlow.App.Pages.Categories
{
    public partial class GetAllCategoriesPage : ComponentBase
    {

        #region Properties
        public bool IsBusy { get; set; } = false;
        public List<Category> Categories { get; set; } = [];
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
                var request = new GetAllCategoriesRequest();
                var result = await _service.GetAllAsync(request);
                if (result.IsSuccess)
                    Categories = result.Data ?? [];
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

        public async void OnDeleteButtonClickedAsync(long id, string title)
        {
            var result = await Dialog.ShowMessageBox(
                "ATENÇÃO",
                $"Ao prosseguir a categoria {title} será removida. Deseja continuar?",
                yesText: "Excluir",
                cancelText: "Cancelar");

            if (result is true)
                await OnDeleteAsync(id, title);

            StateHasChanged();
        }
        public void OnEditButtonClickedAsync(long id)
        {
            OnEditAsync(id);
            StateHasChanged();
        }
        public void OnEditAsync(long id)
        {
            try
            {
                NavigationManager.NavigateTo($"/categories/editar/{id}");
            }
            catch (Exception e)
            {
                Snackbar.Add(e.Message, Severity.Error);
                throw;
            }
        }
        public async Task OnDeleteAsync(long id, string title)
        {
            try
            {
                var request = new DeleteCategoryRequest
                {
                    Id = id
                };
                await _service.DeleteAsync(request);
                Categories.RemoveAll(x => x.Id == id);
                Snackbar.Add($"Categoria {title} removida", Severity.Info);
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
        }
        #endregion
    }
}
