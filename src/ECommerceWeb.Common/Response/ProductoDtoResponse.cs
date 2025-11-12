using System.ComponentModel.DataAnnotations;

namespace ECommerceWeb.Common.Response
{
    public class ProductoDtoResponse
    {
        public int Id { get; set; }
        public string Categoria { get; set; } = null!;
        public string Marca { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public float PrecioUnitario { get; set; }
        public string? UrlImagen { get; set; }
    }
}
