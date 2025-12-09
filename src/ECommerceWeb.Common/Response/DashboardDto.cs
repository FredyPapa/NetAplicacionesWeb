using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceWeb.Common.Response
{
    public class DashboardDto
    {
        public double TotalVenta { get; set; }
        public int CantidadVentas { get; set; }
        public int CantidadClientes { get; set; }
        public int CantidadProductos { get; set; }
    }
}
