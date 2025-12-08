using ECommerceWeb.WebApi.Entities;
using ECommerceWeb.WebApi.Repositories.Interfaces;
using ECommerceWeb.Common.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ECommerceWeb.WebApi.Services;
using ECommerceWeb.Common.Response;

namespace ECommerceWeb.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoRepository _repository;
        private readonly IFileUploader _fileUploader;

        public ProductosController(IProductoRepository repository, IFileUploader fileUploader)
        {
            _repository = repository;
            _fileUploader = fileUploader;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductos(string? filtro)
        {
            var response = new BaseResponse<ICollection<ProductoDtoResponse>>();

            try
            {

                var list = await _repository.ListAsync(
                    predicate: p => p.Nombre.Contains(filtro ?? string.Empty),
                    selector: p => new ProductoDtoResponse
                    {
                        Id = p.Id,
                        Categoria = p.Categoria.Nombre,
                        Marca = p.Marca.Nombre,
                        Descripcion = p.Descripcion,
                        Nombre = p.Nombre,
                        PrecioUnitario = p.PrecioUnitario,
                        UrlImagen = p.UrlImagen
                    });

                response.Data = list;
                response.Success = true;

            }
            catch (Exception ex)
            {
                response.ErrorMessage = $"Error al obtener los productos {ex.Message}";
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProducto(int id)
        {
            var response = new BaseResponse<ProductoDtoRequest>();
            try
            {
                var producto = await _repository.GetByIdAsync(id);
                if (producto == null)
                {
                    return NotFound();
                }

                response.Data = new ProductoDtoRequest
                {
                    Id = producto.Id,
                    CategoriaId = producto.CategoriaId,
                    MarcaId = producto.MarcaId,
                    Nombre = producto.Nombre,
                    Descripcion = producto.Descripcion,
                    PrecioUnitario = producto.PrecioUnitario,
                    UrlImagen = producto.UrlImagen
                };
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = $"Error al obtener el producto {ex.Message}";
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProducto([FromBody] ProductoDtoRequest request)
        {
            var producto = new Producto
            {
                CategoriaId = request.CategoriaId,
                MarcaId = request.MarcaId,
                Nombre = request.Nombre,
                Descripcion = request.Descripcion,
                PrecioUnitario = request.PrecioUnitario,
                UrlImagen = request.UrlImagen
            };

            producto.UrlImagen = await _fileUploader.UploadFileAsync(request.Base64Imagen, request.NombreArchivo);

            await _repository.AddAsync(producto);
            return CreatedAtAction(nameof(GetProducto), new { id = producto.Id }, producto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProducto(int id, [FromBody] ProductoDtoRequest request)
        {
            var producto = await _repository.GetByIdAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            producto.CategoriaId = request.CategoriaId;
            producto.MarcaId = request.MarcaId;
            producto.Nombre = request.Nombre;
            producto.Descripcion = request.Descripcion;
            producto.PrecioUnitario = request.PrecioUnitario;
            producto.UrlImagen = request.UrlImagen;

            if (!string.IsNullOrWhiteSpace(request.Base64Imagen) && !string.IsNullOrWhiteSpace(request.NombreArchivo))
            {
                producto.UrlImagen = await _fileUploader.UploadFileAsync(request.Base64Imagen, request.NombreArchivo);
            }

            await _repository.UpdateAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var producto = await _repository.GetByIdAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(producto.Id);
            return NoContent();
        }
    }
}
