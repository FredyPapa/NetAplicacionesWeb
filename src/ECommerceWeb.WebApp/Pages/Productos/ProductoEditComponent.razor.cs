using ECommerceWeb.Common;
using ECommerceWeb.Common.Request;
using ECommerceWeb.Common.Response;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace ECommerceWeb.WebApp.Pages.Productos
{
    public partial class ProductoEditComponent
    {
        [Parameter]
        public ProductoDtoRequest Model { get; set; } = new ProductoDtoRequest();

        [Parameter]
        public ICollection<MarcaDtoResponse> Marcas { get; set; } = new List<MarcaDtoResponse>();

        [Parameter]
        public ICollection<CategoriaDto> Categorias { get; set; } = new List<CategoriaDto>();

        [Parameter]
        public EventCallback OnGuardar { get; set; }

        private async Task OnFileUploaded(InputFileChangeEventArgs e)
        {
            try
            {
                var archivo = e.File;
                var buffer = new byte[archivo.Size];
                var _ = await archivo.OpenReadStream().ReadAsync(buffer);

                Model.Base64Imagen = Convert.ToBase64String(buffer);
                Model.NombreArchivo = archivo.Name;
                Model.UrlImagen = null!;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}