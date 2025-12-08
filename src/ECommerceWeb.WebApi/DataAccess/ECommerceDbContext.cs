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

            // Data Seeding
            modelBuilder.Entity<Marca>()
                .HasData(new List<Marca>
                {
                new() { Id = 1, Nombre = "Samsung" },
                new() { Id = 2, Nombre = "Apple" },
                new() { Id = 3, Nombre = "Xiaomi" }
                });

            // Data Seeding para Categorias
            /*
            1,Ropa varones,Ropa para el caballero de hoy
            2,Ropa mujer,Ropa para la mujer de hoy
            3,Celulares,Celulares de todas las marcas
            4,Cómputo,"Todos los productos de tecnología de PC, Laptops y Más"
            5,Mascotas,De todo para tu mascota
            6,Bebés,Productos para los más pequeños de la casa
            7,Consolas y videojuegos,Lo mejor de las mejores marcas en videojuegos
            8,Deportes y Fitness,Encuentra de todo para mejorar tu salud
            9,Electrodomésticos,De todo para que tu hogar sea funcional
            10,Música,Todos tus artistas favoritos en un solo lugar

            */

            modelBuilder.Entity<Categoria>()
                .HasData(new List<Categoria>
                {
                new() { Id = 1, Nombre = "Ropa varones", Descripcion = "Ropa para el caballero de hoy" },
                new() { Id = 2, Nombre = "Ropa mujer", Descripcion = "Ropa para la mujer de hoy" },
                new() { Id = 3, Nombre = "Celulares", Descripcion = "Celulares de todas las marcas" },
                new() { Id = 4, Nombre = "Cómputo", Descripcion = "Todos los productos de tecnología de PC, Laptops y Más" },
                new() { Id = 5, Nombre = "Mascotas", Descripcion = "De todo para tu mascota" },
                new() { Id = 6, Nombre = "Bebés", Descripcion = "Productos para los más pequeños de la casa" },
                new() { Id = 7, Nombre = "Consolas y videojuegos", Descripcion = "Lo mejor de las mejores marcas en videojuegos" },
                new() { Id = 8, Nombre = "Deportes y Fitness", Descripcion = "Encuentra de todo para mejorar tu salud" },
                new() { Id = 9, Nombre = "Electrodomésticos", Descripcion = "De todo para que tu hogar sea funcional" },
                new() { Id = 10, Nombre = "Música", Descripcion = "Todos tus artistas favoritos en un solo lugar" }
                });


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

            // Data Seeding para Tipo Cliente
            modelBuilder.Entity<TipoCliente>()
                .HasData(new List<TipoCliente>()
                {
                new() { Id = 1, Descripcion = "Persona Natural"},
                new() { Id= 2, Descripcion = "Persona Jurídica"}
                });


        }

    }
}
