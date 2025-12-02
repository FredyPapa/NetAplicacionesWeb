using Blazored.Toast.Services;
using ECommerceWeb.Common;
using ECommerceWeb.Common.Request;
using ECommerceWeb.Common.Response;
using ECommerceWeb.WebApp.Proxy.Interfaces;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace ECommerceWeb.WebApp.Pages
{
    public partial class Detalle
    {
        private readonly HttpClient _httpClient;
        private readonly IToastService _toastService;
        private readonly ICarritoProxy _carritoProxy;

        public Detalle(HttpClient httpClient, IToastService toastService, ICarritoProxy carritoProxy)
        {
            _httpClient = httpClient;
            _toastService = toastService;
            _carritoProxy = carritoProxy;
        }

        [Parameter]
        public int Id { get; set; }

        public ProductoDtoResponse? Producto { get; set; }

        public int Cantidad { get; set; } = 1;

        public bool IsLoading { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            try
            {
                IsLoading = true;
                var response = await _httpClient.GetFromJsonAsync<BaseResponse<ProductoDtoRequest>>($"api/productos/{Id}");
                if(response is { Success:true, Data: not null })
                {
                    Producto = new ProductoDtoResponse
                    {
                        Id = response.Data.Id,
                        Nombre = response.Data.Nombre,
                        Descripcion = response.Data.Descripcion,
                        PrecioUnitario = response.Data.PrecioUnitario,
                        UrlImagen = response.Data.UrlImagen,
                    };
                }
                else
                {
                    _toastService.ShowError("No se pudo cargar el detalle del producto");
                }
            }
            catch (Exception ex)
            {
                _toastService.ShowError($"Error: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task AgregarAlCarrito()
        {
            try
            {
                if (Producto is null)
                    return;

                var carritoDto = new CarritoDto
                {
                    ProductoDto = Producto,
                    Cantidad = Cantidad > 0 ? Cantidad : 1,
                    Precio = Producto.PrecioUnitario,
                    Total = Producto.PrecioUnitario * Cantidad
                };
                //
                await _carritoProxy.AgregarCarrito(carritoDto);
            }
            catch(Exception ex)
            {
                _toastService.ShowError($"Error al agregar al carrito: {ex.Message}");
            }

        }
    }
}