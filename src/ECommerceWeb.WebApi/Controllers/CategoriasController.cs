using ECommerceWeb.Common.Request;
using ECommerceWeb.Common.Response;
using ECommerceWeb.WebApi.DataAccess;
using ECommerceWeb.WebApi.Entities;
using ECommerceWeb.WebApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> GetCategorias(string? filtro)
        {
            var response = new BaseResponse<ICollection<CategoriaDtoResponse>>();

            try
            {
                var lista = await _repository.ListAsync(p => p.Nombre
                                    .Contains(filtro ?? string.Empty));

                response.Data = lista.Select(c => new CategoriaDtoResponse
                {
                    Id = c.Id,
                    Nombre = c.Nombre,
                    Descripcion = c.Descripcion
                }).ToList();

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategoria([FromBody] CategoriaDtoRequest categoria)
        {
            try
            {
                var entity = new Categoria
                {
                    Nombre = categoria.Nombre,
                    Descripcion = categoria.Descripcion
                };
                await _repository.AddAsync(entity);
                return CreatedAtAction(nameof(GetCategorias), new { id = entity.Id }, entity);
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
                if (categoria == null)
                {
                    return NotFound();
                }
                return Ok(categoria);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "No se pudo obtener el registro", Error = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCategoria(int id, [FromBody] CategoriaDtoRequest categoria)
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
