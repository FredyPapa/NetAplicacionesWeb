using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceWeb.Common.Request
{
    public class VentaDtoRequest
    {
        public float Total { get; set; }
        public HashSet<VentaDetalleDto> VentaDetalles { get; set; } = new();
    }

    public class VentaDetalleDto
    {
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public float Precio { get; set; }
        public float Total { get; set; }
    }
}
