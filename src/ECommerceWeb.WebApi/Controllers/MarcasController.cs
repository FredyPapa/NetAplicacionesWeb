using System;
using ECommerceWeb.Common.Response;
using ECommerceWeb.WebApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceWeb.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcasController : ControllerBase
    {
        private readonly IMarcaRepository _repository;
        private readonly ILogger<MarcasController> _logger;

        public MarcasController(IMarcaRepository repository, ILogger<MarcasController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string? filtro)
        {
            var response = new BaseResponse<ICollection<MarcaDtoResponse>>();

            try
            {
                var lista = await _repository.ListAsync(p => p.Nombre
                                    .Contains(filtro ?? string.Empty)
                                    );

                response.Data = lista.Select(p => new MarcaDtoResponse
                {
                    Id = p.Id,
                    Nombre = p.Nombre
                }).ToList();
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Error al listar");
                response.ErrorMessage = ex.Message;
            }

            return Ok(response);
        }
    }
}
