using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceWeb.Common.Request
{
    public class RegisterUserDto
    {
        [Required(ErrorMessage = Constantes.CampoRequerido)]
        public string Usuario { get; set; } = string.Empty;

        [Required(ErrorMessage = Constantes.CampoRequerido)]
        public string NombreCompleto { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = Constantes.CampoRequerido)]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = Constantes.CampoRequerido)]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = Constantes.CampoRequerido)]
        public string ConfirmarPassword { get; set; } = null!;

        public DateOnly FechaNacimiento { get; set; }

        [StringLength(250)]
        public string? Direccion { get; set; }
    }
}
