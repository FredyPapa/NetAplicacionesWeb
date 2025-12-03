using ECommerceWeb.Common;
using ECommerceWeb.Common.Request;
using ECommerceWeb.Common.Response;
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
        private readonly IVentaRepository _ventasRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly ILogger<VentasController> _logger;

        public VentasController(IVentaRepository ventasRepository, IClienteRepository clienteRepository, ILogger<VentasController> logger)
        {
            _ventasRepository = ventasRepository;
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

            }
            catch (Exception ex)
            {

            }
        }
    }
}
