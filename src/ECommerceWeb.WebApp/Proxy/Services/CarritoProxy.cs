using Blazored.LocalStorage;
using Blazored.Toast.Services;
using ECommerceWeb.Common;
using ECommerceWeb.WebApp.Proxy.Interfaces;

namespace ECommerceWeb.WebApp.Proxy.Services
{
    public class CarritoProxy : ICarritoProxy
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly ISyncLocalStorageService _syncLocalStorageService;
        private readonly IToastService _toastService;

        public CarritoProxy(ILocalStorageService localStorageService, ISyncLocalStorageService syncLocalStorageService, IToastService toastService)
        {
            _localStorageService = localStorageService;
            _syncLocalStorageService = syncLocalStorageService;
            _toastService = toastService;
        }

        public event Action? ActualizarVista;

        public async Task AgregarCarrito(CarritoDto carrito)
        {
            try
            {
                var cart = await _localStorageService.GetItemAsync<ICollection<CarritoDto>>("carrito") ?? new List<CarritoDto>();

                var producto = cart.FirstOrDefault(x => x.ProductoDto.Id == carrito.ProductoDto.Id);
                if(producto is not null)
                    cart.Remove(producto);

                cart.Add(carrito);
                await _localStorageService.SetItemAsync("carrito", cart);
                ActualizarVista?.Invoke();

                _toastService.ShowSuccess(producto is not null ? "Producto actualizado en el carrito" : "Producto agregado al carrito");
            }catch(Exception ex)
            {
                _toastService.ShowError("Nos e pudo agregar el producto al carrito");
            }
        }

        public int CantidadProductos()
        {
            var carrito = _syncLocalStorageService.GetItem<ICollection<CarritoDto>>("carrito");
            return carrito?.Count ?? 0;
        }

        public async Task EliminarCarrito(int idProducto)
        {
            try
            {
                var cart = await _localStorageService.GetItemAsync<ICollection<CarritoDto>>("carrito") ?? new List<CarritoDto>();

                var producto = cart.FirstOrDefault(x=>x.ProductoDto.Id ==idProducto);
                if(producto is not null)
                {
                    cart.Remove(producto);
                    await _localStorageService.SetItemAsync("carrito", cart);
                    ActualizarVista?.Invoke();
                    _toastService.ShowSuccess("Producto eliminado del carrito");
                }
                else
                {
                    _toastService.ShowWarning("El producto no se encuentra en el carrito");
                }
            }catch(Exception ex)
            {
                _toastService.ShowError("No se pudo eliminar el producto del carrito");
            }
        }

        public async Task LimpiarCarrito()
        {
            try
            {
                await _localStorageService.RemoveItemAsync("carrito");
                ActualizarVista?.Invoke();
            }
            catch(Exception ex)
            {
                _toastService.ShowError("No se pudo limpiar el carrito");
            }
        }

        public async Task<ICollection<CarritoDto>> ObtenerCarrito()
        {
            try
            {
                var carrito = await _localStorageService.GetItemAsync<ICollection<CarritoDto>>("carrito");
                return carrito ?? new List<CarritoDto>();
            }
            catch(Exception ex)
            {
                _toastService.ShowError("No se pudo obtener el carrito");
                return new List<CarritoDto>();
            }
        }
    }
}
