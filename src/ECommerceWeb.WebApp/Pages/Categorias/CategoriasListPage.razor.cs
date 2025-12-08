using System.Net.Http.Json;
using CurrieTechnologies.Razor.SweetAlert2;
using ECommerceWeb.Common;
using ECommerceWeb.Common.Response;

namespace ECommerceWeb.WebApp.Pages.Categorias
{
    public partial class CategoriasListPage
    {
        public ICollection<CategoriaDtoResponse> Categorias { get; set; } = new List<CategoriaDtoResponse>();
        public bool IsLoading { get; set; }
        public string? Filtro { get; set; }

        private async Task CargarDatos()
        {
            try
            {
                IsLoading = true;
                var response =
                  await HttpClient.GetFromJsonAsync<BaseResponse<ICollection<CategoriaDtoResponse>>>
                      ($"api/Categorias?filtro={Filtro}");

                // Pattern Matching
                if (response is { Data: not null, Success: true })
                {
                    Categorias = response.Data;
                }
                else if (response is { ErrorMessage: not null })
                {
                    ToastService.ShowError(response.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                ToastService.ShowError($"Ocurrió un error: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }

        }

        protected override async Task OnInitializedAsync()
        {
            await CargarDatos();
        }

        private void Editar(int id)
        {
            NavigationManager.NavigateTo($"categorias/edit/{id}");
        }

        private async Task Eliminar(int id)
        {
            var result = await Swal.FireAsync(new SweetAlertOptions("Eliminar")
            {
                Text = "¿Desea eliminar el registro?",
                Icon = SweetAlertIcon.Question,
                ConfirmButtonText = "Sí",
                CancelButtonText = "No",
                ShowCancelButton = true
            });

            if (!result.IsConfirmed)
                return;

            var response = await HttpClient.DeleteFromJsonAsync<BaseResponse>($"api/Categorias/{id}");
            if (response is { Success: true })
            {
                await CargarDatos();
            }
            else
            {
                ToastService.ShowError("No se pudo eliminar el registro");
            }
        }
    }
}