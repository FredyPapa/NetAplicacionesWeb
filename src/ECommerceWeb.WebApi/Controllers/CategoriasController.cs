using ECommerceWeb.WebApi.DataAccess;
using ECommerceWeb.WebApi.Entities;
using ECommerceWeb.WebApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWeb.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaRepository _repository;

        public CategoriasController(ICategoriaRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategorias()
        {
            try
            {
                var categorias = await _repository.ListAsync();
                return Ok(categorias);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "No se pudo listar los registros", Error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategoria([FromBody] Categoria categoria)
        {
            try
            {
                await _repository.AddAsync(categoria);
                return CreatedAtAction(nameof(GetCategorias), new { id = categoria.Id }, categoria);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "No se pudo crear el registro", Error = ex.Message });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCategoriaById(int id)
        {
            try
            {
                var categoria = await _repository.GetByIdAsync(id);
                if(categoria == null)
                {
                    return NotFound();
                }
                return Ok(categoria);
            }
            catch (Exception ex)
            {
                return BadRequest(new {Message = "No se pudo obtener el registro", Error = ex.Message});
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCategoria(int id, [FromBody] Categoria categoria)
        {
            try
            {
                var existingCategoria = await _repository.GetByIdAsync(id);
                if (existingCategoria == null)
                {
                    return NotFound();
                }
                existingCategoria.Nombre = categoria.Nombre;
                existingCategoria.Descripcion = categoria.Descripcion;

                await _repository.UpdateAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "No se pudo actualizar el registro", Error = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            try
            {
                var existingCategoria = await _repository.GetByIdAsync(id);
                if (existingCategoria == null)
                {
                    return NotFound();
                }

                await _repository.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "No se pudo eliminar el registro", Error = ex.Message });
            }
        }
    }
}
