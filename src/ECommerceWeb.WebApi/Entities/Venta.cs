namespace ECommerceWeb.WebApi.Entities
{
    public class Venta:EntityBase
    {
        public Venta()
        {
            FechaCreacion = DateTime.Now;
        }

        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; } = null!;
        public float Total { get; set; }
        public DateTime FechaCreacion { get; set; }

        //Esto no forma parte de la tabla en sí, sólo sirve para la migración
        public HashSet<VentaDetalle> VentaDetalle { get; set; } = new();
    }

}
