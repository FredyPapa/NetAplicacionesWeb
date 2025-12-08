using System.Security.Claims;
using ECommerceWeb.Common;
using ECommerceWeb.Common.Request;
using ECommerceWeb.Common.Response;
using ECommerceWeb.WebApi.Entities;
using ECommerceWeb.WebApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceWeb.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        private readonly IVentaRepository _repository;
        private readonly IClienteRepository _clienteRepository;
        private readonly ILogger<VentasController> _logger;

        public VentasController(IVentaRepository repository, IClienteRepository clienteRepository, ILogger<VentasController> logger)
        {
            _repository = repository;
            _clienteRepository = clienteRepository;
            _logger = logger;
        }

        [HttpPost]
        [Authorize(Roles = Constantes.RolCliente)]
        public async Task<IActionResult> Post([FromBody] VentaDtoRequest request)
        {
            var response = new BaseResponse();

            try
            {
                // Buscamos el ID del cliente basado en el correo electronico del usuario autenticado
                var email = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Email).Value;
                var cliente = await _clienteRepository.BuscarPorEmailAsync(email);

                if (cliente is null)
                {
                    response.ErrorMessage = $"El cliente con el correo {email} no existe!";
                    return BadRequest(response);
                }

                var venta = new Venta
                {
                    ClienteId = cliente.Id,
                    Total = request.Total,
                    VentaDetalles = request.VentaDetalles.Select(x => new VentaDetalle
                    {
                        ProductoId = x.ProductoId,
                        Cantidad = x.Cantidad,
                        Precio = x.Precio,
                        Total = x.Total
                    }).ToHashSet()
                };

                await _repository.CrearTransaccionAsync();
                _ = await _repository.AddAsync(venta);

                await _repository.UpdateAsync();
                await _repository.ConfirmarTransaccionAsync();

                response.Success = true;

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al crear la venta";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
                await _repository.ResetearTransaccionAsync();
                return BadRequest(response);
            }
        }
    }
}
