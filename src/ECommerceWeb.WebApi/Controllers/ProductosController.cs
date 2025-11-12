using ECommerceWeb.WebApi.Entities;
using ECommerceWeb.WebApi.Repositories.Interfaces;
using ECommerceWeb.Common.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceWeb.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoRepository _repository;

        public ProductosController(IProductoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductos()
        {
            var productos = await _repository.ListAsync();
            return Ok(productos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProducto(int id)
        {
            var producto = await _repository.GetByIdAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            return Ok(producto);
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
            await _repository.AddAsync(producto);
            return CreatedAtAction(nameof(GetProducto), new {id = producto.Id}, producto);
        }
    }
}
