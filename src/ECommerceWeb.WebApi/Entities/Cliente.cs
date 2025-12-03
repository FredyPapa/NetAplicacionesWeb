namespace ECommerceWeb.WebApi.Entities
{
    public class Cliente:EntityBase
    {
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateOnly FechaNacimiento { get; set; }
        public TipoCliente TipoCliente { get; set; } = null!;
        public int TipoClienteId { get; set; }
    }
}
