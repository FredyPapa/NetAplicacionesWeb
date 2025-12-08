using System;
using System.Linq.Expressions;
using ECommerceWeb.WebApi.Entities;

namespace ECommerceWeb.WebApi.Repositories.Interfaces
{
    public interface IClienteRepository:IRepositoryBase<Cliente>
    {
        Task<Cliente?> BuscarPorEmailAsync(string email);
        Task<ICollection<Cliente>> ListarConTipoClienteAsync(Expression<Func<Cliente, bool>> predicate);
    }
}
