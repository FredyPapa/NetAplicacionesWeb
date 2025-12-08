using System.Net.Http.Json;
using CurrieTechnologies.Razor.SweetAlert2;
using ECommerceWeb.Common;
using ECommerceWeb.Common.Response;

namespace ECommerceWeb.WebApp.Pages.Clientes
{
    public partial class ClientesListPage
    {
        public ICollection<ClienteDtoResponse> Clientes { get; set; } = new List<ClienteDtoResponse>();
        public bool IsLoading { get; set; }
        public string? Filtro { get; set; }

        private async Task CargarDatos()
        {
            try
            {
                IsLoading = true;
                var response =
                  await HttpClient.GetFromJsonAsync<BaseResponse<ICollection<ClienteDtoResponse>>>
                      ($"api/Clientes?filtro={Filtro}");

                // Pattern Matching
                if (response is { Data: not null, Success: true })
                {
                    Clientes = response.Data;
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
    }
}