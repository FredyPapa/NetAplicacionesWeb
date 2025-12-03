using ECommerceWeb.WebApi.Entities;

namespace ECommerceWeb.WebApi.Repositories.Interfaces
{
    public interface IVentaRepository:IRepositoryBase<Venta>
    {
        Task CrearTransaccionAsync();
        Task ConfirmarTransaccionAsync();
        Task ResetearTransaccionAsync();
    }
}
