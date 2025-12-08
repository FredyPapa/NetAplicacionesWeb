using ECommerceWeb.Common;
using ECommerceWeb.Common.Request;
using ECommerceWeb.Common.Response;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace ECommerceWeb.WebApp.Pages.Productos
{
    public partial class ProductoEditComponent
    {
        [Parameter] public ProductoDtoRequest Request { get; set; } = new();

        [Parameter] public EventCallback OnGrabar { get; set; }

        [Parameter] public string Titulo { get; set; } = string.Empty;

        [Parameter]
        public ICollection<CategoriaDtoResponse> Categorias { get; set; } = new List<CategoriaDtoResponse>();

        [Parameter]
        public ICollection<MarcaDtoResponse> Marcas { get; set; } = new List<MarcaDtoResponse>();

        private string TextoBoton { get; set; } = "Crear";

        private void Grabar()
        {
            OnGrabar.InvokeAsync();
        }

        protected override void OnInitialized()
        {
            TextoBoton = Request.Id == 0 ? "Crear" : "Actualizar";
        }

        private async Task OnFileUploaded(InputFileChangeEventArgs e)
        {
            try
            {
                var imagen = e.File;
                var buffer = new byte[imagen.Size];
                var _ = await imagen.OpenReadStream().ReadAsync(buffer); // Extrae el base64

                Request.Base64Imagen = Convert.ToBase64String(buffer);
                Request.NombreArchivo = imagen.Name;
                Request.UrlImagen = null;
            }
            catch (Exception ex)
            {
                ToastService.ShowError(ex.Message);
            }
        }
    }
}