namespace ECommerceWeb.WebApi.Entities
{
    public class Categoria:EntityBase
    {
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; }=string.Empty;
    }
}
