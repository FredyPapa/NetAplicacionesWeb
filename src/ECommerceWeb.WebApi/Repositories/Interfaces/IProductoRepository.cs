using ECommerceWeb.WebApi.Entities;
using ECommerceWeb.WebApi.Entities.Infos;

namespace ECommerceWeb.WebApi.Repositories.Interfaces
{
    public interface IProductoRepository:IRepositoryBase<Producto>
    {
        Task<ICollection<ProductoInfo>> GetAllProductsAsync();
    }
}
