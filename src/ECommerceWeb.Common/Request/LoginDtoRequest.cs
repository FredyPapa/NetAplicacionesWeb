using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceWeb.Common.Request
{
    public class LoginDtoRequest
    {
        [Required(ErrorMessage = Constantes.CampoRequerido)]
        [Display(Name = "Nombre de usuario")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = Constantes.CampoRequerido)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; } = string.Empty;
    }
}
