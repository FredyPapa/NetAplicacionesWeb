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
            var categorias = await _repository.ListAsync();
            return Ok(categorias);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategoria([FromBody] Categoria categoria)
        {
            await _repository.AddAsync(categoria);
            return CreatedAtAction(nameof(GetCategorias), new { id = categoria.Id }, categoria);
        }
    }
}
