using ECommerceWeb.WebApi.Entities;

namespace ECommerceWeb.WebApi.Repositories.Interfaces
{
    public interface IClienteRepository:IRepositoryBase<Cliente>
    {
        Task<Cliente?> BuscarPorEmailAsync(string email);
    }
}
