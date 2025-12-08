using System.Net.Http.Json;
using Blazored.Toast.Services;
using ECommerceWeb.Common.Response;
using Microsoft.AspNetCore.Components;

namespace ECommerceWeb.WebApp.Pages
{
    public partial class Home
    {
        private readonly IToastService _toastService;
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;

        public Home(IToastService toastService, HttpClient httpClient, NavigationManager navigationManager)
        {
            _toastService = toastService;
            _httpClient = httpClient;
            _navigationManager = navigationManager;
        }

        private ICollection<ProductoDtoResponse>? Productos { get; set; }
        private ICollection<CategoriaDtoResponse>? Categorias { get; set; }

        private string Buscar { get; set; } = string.Empty;

        private bool IsLoading { get; set; }


        private async Task ObtenerCatalogo()
        {

            try
            {
                IsLoading = true;
                var response = await _httpClient.GetAsync($"api/productos?filtro={Buscar}");
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<BaseResponse<ICollection<ProductoDtoResponse>>>();
                if (result is { Success: true, Data: not null })
                {
                    Productos = result.Data;
                }
                else
                {
                    _toastService.ShowError(result?.ErrorMessage ?? "Error al obtener los productos");
                }
            }
            catch (Exception ex)
            {
                _toastService.ShowError(ex.Message);
            }
            finally
            {
                IsLoading = false;
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await ObtenerCatalogo();

            var response = await _httpClient.GetFromJsonAsync<BaseResponse<ICollection<CategoriaDtoResponse>>>("api/categorias");
            if (response is { Success: true, Data: not null })
            {
                Categorias = response.Data;
            }
            else
            {
                _toastService.ShowError(response?.ErrorMessage ?? "Error al obtener las categorías");
            }
        }

        private void VerDetalle(int id)
        {
            _navigationManager.NavigateTo($"/detalle/{id}");
        }
    }
}
