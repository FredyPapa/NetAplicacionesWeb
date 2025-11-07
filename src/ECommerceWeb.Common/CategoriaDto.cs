using System.ComponentModel.DataAnnotations;

namespace ECommerceWeb.Common
{
    public class CategoriaDto
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; } = string.Empty;
        [Required]
        public string Descripcion { get; set;} = string.Empty;
    }
}
