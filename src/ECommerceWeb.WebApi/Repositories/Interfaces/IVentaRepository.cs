using ECommerceWeb.WebApi.Entities;
using ECommerceWeb.WebApi.Entities.Infos;

namespace ECommerceWeb.WebApi.Repositories.Interfaces
{
    public interface IVentaRepository:IRepositoryBase<Venta>
    {
        Task CrearTransaccionAsync();
        Task ConfirmarTransaccionAsync();
        Task ResetearTransaccionAsync();
        Task<Dashboard> MostrarDashboard();
    }
}
