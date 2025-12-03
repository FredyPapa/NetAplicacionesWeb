using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceWeb.Common.Request
{
    public class TarjetaDto
    {
        [Required(ErrorMessage = Constantes.CampoRequerido)]
        [Display(Name = "Número de tarjeta")]
        public string Numero { get; set; } = null!;

        [Required(ErrorMessage = Constantes.CampoRequerido)]
        [Display(Name = "Nombre en la tarjeta")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = Constantes.CampoRequerido)]
        [Display(Name = "Fecha de expiración")]
        public string FechaExpiracion { get; set; } = null!;

        [Required(ErrorMessage = Constantes.CampoRequerido)]
        [Display(Name = "CVV")]
        public string CodigoSeguridad { get; set; } = null!;
    }
}
