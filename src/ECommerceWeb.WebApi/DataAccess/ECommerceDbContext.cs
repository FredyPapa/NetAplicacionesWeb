using ECommerceWeb.WebApi.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWeb.WebApi.DataAccess
{
    public class ECommerceDbContext : IdentityDbContext<ECommerceUserIdentity>     //DbContext
    {
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options)
        {
        }

        public DbSet<Categoria> Categorias { get; set; } = null!;
        public DbSet<Marca> Marcas { get; set; } = null!;
        public DbSet<Producto> Productos { get; set; } = null!;

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);

            configurationBuilder.Properties<string>()
                .HaveMaxLength(100);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Producto>()
                .Property(p => p.UrlImagen)
                .HasMaxLength(500);

            //Data Seeding
            modelBuilder.Entity<Marca>()
                .HasData(new List<Marca>
                {
                    new () {Id=11,Nombre="Samsung 2" },
                    new () {Id=12,Nombre="Apple 2" },
                    new () {Id=13,Nombre="Xiaomi 2" }
                });

            //

            modelBuilder.Entity<Cliente>()
                .Property(p => p.Nombres)
                .HasMaxLength(100);

            modelBuilder.Entity<Cliente>()
                .Property(p => p.Apellidos)
                .HasMaxLength(100);

            modelBuilder.Entity<Cliente>()
                .Property(p => p.Email)
                .HasMaxLength(500);

            modelBuilder.Entity<Venta>()
                .ToTable(nameof(Venta));

            //Data Seeding para Tipo Cliente
            modelBuilder.Entity<TipoCliente>()
                .HasData(new List<TipoCliente>()
                {
                    new() {Id=1, Descripcion = "Persona Natural" },
                    new() {Id=2, Descripcion = "Persona Jurídica" }
                });
        }

    }
}
