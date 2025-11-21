using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ECommerceWeb.WebApi.DataAccess
{
    public class ECommerceUserIdentity:IdentityUser
    {
        [StringLength(200)]
        public string NombreCompleto { get; set; } = string.Empty;

        public DateOnly FechaNacimiento { get; set; }

        [StringLength(200)]
        public string? Direccion { get; set; }
    }
}
