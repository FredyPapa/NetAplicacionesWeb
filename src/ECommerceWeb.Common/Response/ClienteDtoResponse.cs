using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceWeb.Common.Response
{
    public class ClienteDtoResponse
    {
        public int Id { get; set; }
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateOnly FechaNacimiento { get; set; }
        public int TipoClienteId { get; set; }
        public string TipoClienteDescripcion { get; set; } = null!;
        public string NombreCompleto => $"{Nombres} {Apellidos}";
    }
}
