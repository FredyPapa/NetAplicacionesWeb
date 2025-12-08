using System.Net.Http.Json;
using CurrieTechnologies.Razor.SweetAlert2;
using ECommerceWeb.Common;
using ECommerceWeb.Common.Response;

namespace ECommerceWeb.WebApp.Pages.Marcas
{
    public partial class MarcasListPage
    {
        public ICollection<MarcaDtoResponse> Marcas { get; set; } = new List<MarcaDtoResponse>();
        public bool IsLoading { get; set; }
        public string? Filtro { get; set; }

        private async Task CargarDatos()
        {
            try
            {
                IsLoading = true;
                var response =
                  await HttpClient.GetFromJsonAsync<BaseResponse<ICollection<MarcaDtoResponse>>>
                      ($"api/Marcas?filtro={Filtro}");

                // Pattern Matching
                if (response is { Data: not null, Success: true })
                {
                    Marcas = response.Data;
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