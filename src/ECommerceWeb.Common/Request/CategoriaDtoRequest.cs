using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceWeb.Common.Request
{
    public class CategoriaDtoRequest
    {
        [Required(ErrorMessage = "Campo {0} es requerido")]
        [StringLength(100, ErrorMessage = "El campo {0} no puede superar la cantidad maxima de {1} de caracteres")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "Campo {0} es requerido")]
        public string Descripcion { get; set; } = null!;
    }
}
