using Microsoft.EntityFrameworkCore;

namespace ECommerceWeb.WebApi.DataAccess
{
    public class ECommerceDbContext : DbContext
    {
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options)
        {
        }

        public DbSet<Entities.Categoria> Categorias { get; set; } = null!;
        
    }
}
