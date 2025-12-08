using System;
using ECommerceWeb.Common.Response;
using ECommerceWeb.WebApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceWeb.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteRepository _repository;
        private readonly ILogger<ClientesController> _logger;

        public ClientesController(IClienteRepository repository, ILogger<ClientesController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string? filtro)
        {
            var response = new BaseResponse<ICollection<ClienteDtoResponse>>();

            try
            {
                var lista = await _repository.ListarConTipoClienteAsync(
                    p => (p.Nombres + " " + p.Apellidos).Contains(filtro ?? string.Empty)
                );

                response.Data = lista.Select(p => new ClienteDtoResponse
                {
                    Id = p.Id,
                    Nombres = p.Nombres,
                    Apellidos = p.Apellidos,
                    Email = p.Email,
                    FechaNacimiento = p.FechaNacimiento,
                    TipoClienteId = p.TipoClienteId,
                    TipoClienteDescripcion = p.TipoCliente.Descripcion
                }).ToList();
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Error al listar clientes");
                response.ErrorMessage = ex.Message;
            }

            return Ok(response);
        }
    }
}
