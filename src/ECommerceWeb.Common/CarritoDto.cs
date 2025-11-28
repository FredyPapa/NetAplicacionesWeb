using ECommerceWeb.Common.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceWeb.Common
{
    public class CarritoDto
    {
        public ProductoDtoResponse ProductoDto { get; set; } = null!;
        public int Cantidad { get; set; }
        public float Precio { get; set; }
        public float Total { get; set; }
    }
}
