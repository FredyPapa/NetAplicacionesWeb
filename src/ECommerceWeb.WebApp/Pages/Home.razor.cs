using Blazored.Toast.Services;
using ECommerceWeb.Common.Response;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace ECommerceWeb.WebApp.Pages
{
    public partial class Home
    {
        private readonly IToastService _toastService;
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;

        public Home(IToastService toastService,HttpClient httpClient, NavigationManager navigation)
        {
            _toastService = toastService;
            _httpClient = httpClient;
            _navigationManager = navigation;
        }

        public string SearchTerm { get; set; } = null!;

        public ICollection<ProductoDtoResponse>? Productos { get; set; }

        public bool IsLoading { get; set; }


        private async Task SearchProducts()
        {
            try
            {
                IsLoading = true;
                //
                var response = await _httpClient.GetAsync($"api/Productos");
                if (response.IsSuccessStatusCode)
                {
                    Productos = await response.Content.ReadFromJsonAsync<ICollection<ProductoDtoResponse>>() ?? new List<ProductoDtoResponse>();
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
            await SearchProducts();
        }

        private void VerDetalle(int idProducto)
        {
            _navigationManager.NavigateTo($"/detalle/{idProducto}");
        }
    }
}
