using System.ComponentModel.DataAnnotations;

namespace ECommerceWeb.WebApi.Entities
{
    public class Producto:EntityBase
    {
        public Categoria Categoria { get; set; } = null!;
        public Marca Marca { get; set; } = null!;
        public int CategoriaId { get; set; }
        public int MarcaId { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public float PrecioUnitario { get; set; }
        [StringLength(300)]
        public string? UrlImagen { get; set; }
    }
}
