using System.Net.Http.Json;
using CurrieTechnologies.Razor.SweetAlert2;
using ECommerceWeb.Common.Response;

namespace ECommerceWeb.WebApp.Pages.Productos
{
    public partial class ProductosListPage
    {
        public ICollection<ProductoDtoResponse>? ProductosList { get; set; }
        public string? Filtro { get; set; }

        private async Task CargarDatos()
        {
            try
            {
                var response =
                    await HttpClient.GetFromJsonAsync<BaseResponse<ICollection<ProductoDtoResponse>>>
                        ($"api/Productos?filtro={Filtro}");

                if (response is { Data: not null, Success: true })
                {
                    ProductosList = response.Data;
                }
                else if (response is { ErrorMessage: not null })
                {
                    ToastService.ShowError(response.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                ToastService.ShowError(ex.Message);
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await CargarDatos();
        }

        private void Editar(int id)
        {
            NavigationManager.NavigateTo($"productos/edit/{id}");
        }

        private async Task Eliminar(int id)
        {
            try
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

                var response = await HttpClient.DeleteFromJsonAsync<BaseResponse>($"api/Productos/{id}");
                if (response is { Success: true })
                {
                    await CargarDatos();
                }
                else
                {
                    ToastService.ShowError("No se pudo eliminar el registro");
                }
            }
            catch (Exception ex)
            {
                ToastService.ShowError(ex.Message);
            }
        }


    }
}