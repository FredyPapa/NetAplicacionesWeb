using ECommerceWeb.WebApi.DataAccess;
using ECommerceWeb.WebApi.Entities;
using ECommerceWeb.WebApi.Entities.Infos;
using ECommerceWeb.WebApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWeb.WebApi.Repositories.Services
{
    public class ProductoRepository : RepositoryBase<Producto>, IProductoRepository
    {
        public ProductoRepository(ECommerceDbContext context) : base(context)
        {
        }

        public async Task<ICollection<ProductoInfo>> GetAllProductsAsync()
        {
            /*
            // Es una manera menos eficiente por no tener un Select
            var _ = await _context.Set<Producto>()
                .Include(p => p.Categoria)
                .Include(p => p.Marca)
                .ToListAsync();
            */

            return await _context.Set<Producto>()
                .Include(p => p.Categoria)
                .Include(p => p.Marca)
                .Select(p => new ProductoInfo
                {
                    Id = p.Id,
                    Categoria = p.Categoria!.Nombre,
                    Marca = p.Marca!.Nombre,
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion,
                    PrecioUnitario = p.PrecioUnitario,
                    UrlImagen = p.UrlImagen
                })
                .ToListAsync();
        }
    }
}
