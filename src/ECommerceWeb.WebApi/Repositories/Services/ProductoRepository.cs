using ECommerceWeb.WebApi.DataAccess;
using ECommerceWeb.WebApi.Entities;
using ECommerceWeb.WebApi.Repositories.Interfaces;

namespace ECommerceWeb.WebApi.Repositories.Services
{
    public class ProductoRepository : RepositoryBase<Producto>, IProductoRepository
    {
        public ProductoRepository(ECommerceDbContext context) : base(context)
        {
        }
    }
}
