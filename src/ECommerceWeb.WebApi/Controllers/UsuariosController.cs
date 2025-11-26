using ECommerceWeb.Common.Request;
using ECommerceWeb.WebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceWeb.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUserService _service;

        public UsuariosController(IUserService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDtoRequest request)
        {
            var response = await _service.LoginAsync(request);

            return response.Success ? Ok(response) : Unauthorized(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto request)
        {
            var response = await _service.RegisterAsync(request);
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
    }
}
