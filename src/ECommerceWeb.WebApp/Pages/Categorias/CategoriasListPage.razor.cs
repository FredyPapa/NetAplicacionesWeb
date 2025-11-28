using CurrieTechnologies.Razor.SweetAlert2;
using ECommerceWeb.Common;
using System.Net.Http.Json;

namespace ECommerceWeb.WebApp.Pages.Categorias
{
    public partial class CategoriasListPage
    {
        public ICollection<CategoriaDto> Categorias { get; set; } = new List<CategoriaDto>();

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            try
            {
                //var HttpResponseMessage? response = await HttpClient.GetAsync("api/categorias");
                var response = await HttpClient.GetAsync("api/categorias");
                if (response.IsSuccessStatusCode)
                {
                    Categorias = await response.Content.ReadFromJsonAsync<ICollection<CategoriaDto>>()
                    ?? new List<CategoriaDto>();
                }
            }
            catch(Exception ex)
            {
                ToastService.ShowError(ex.Message);
            }
        }

        private async Task DeleteCategoria(int id)
        {
            var swalResult = await Swal.FireAsync(new SweetAlertOptions
            {
                Title = "¿Estás seguro?",
                Text = "Esta acción no se puede deshacer",
                Icon = SweetAlertIcon.Warning,
                ShowCancelButton = true,
                ConfirmButtonText = "Sí, eliminar",
                CancelButtonText = "Cancelar"
            });

            if (!swalResult.IsConfirmed)
                return;

            var response = await HttpClient.DeleteAsync($"api/categorias/{id}");
            if (response.IsSuccessStatusCode)
            {
                //Refrescar la lista luego de eliminar
                await LoadData();
            }
        }
    }
}