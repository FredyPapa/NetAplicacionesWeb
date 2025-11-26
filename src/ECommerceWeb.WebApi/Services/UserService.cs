using Azure;
using ECommerceWeb.Common;
using ECommerceWeb.Common.Request;
using ECommerceWeb.Common.Response;
using ECommerceWeb.WebApi.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security;
using System.Security.Claims;
using System.Text;

namespace ECommerceWeb.WebApi.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ECommerceUserIdentity> _userManager;
        private readonly ILogger<UserService> _logger;

        public UserService(IConfiguration configuration, UserManager<ECommerceUserIdentity> userManager,ILogger<UserService> logger)
        {
            _configuration = configuration;
            _userManager = userManager;
            _logger = logger;
        }
        public async Task<LoginDtoResponse> LoginAsync(LoginDtoRequest request)
        {
            var response = new LoginDtoResponse();

            try
            {
                var user = await _userManager.FindByNameAsync(request.UserName);
                if (user == null)
                    throw new SecurityException("Usuario no encontrado");

                var result = await _userManager.CheckPasswordAsync(user, request.Password);
                if (!result)
                {
                    throw new SecurityException("Contraseña incorrecta:");
                }

                //En caso sí encontró el usuario y la contraseña es correcta
                var fechaExiracion = DateTime.UtcNow.AddHours(1);
                var roles = await _userManager.GetRolesAsync(user);

                //Vamos a devolver los Claims del usuario (claims son datos adicionales)
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.NombreCompleto),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Expiration, fechaExiracion.ToString("yyyy-MM-dd HH:mm:ss")),
                };

                claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

                //Generar token
                var llaveSimetrica = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));
                var credenciales = new SigningCredentials(llaveSimetrica, SecurityAlgorithms.HmacSha256);

                var header = new JwtHeader(credenciales);
                var payload = new JwtPayload(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.UtcNow.AddHours(1)
                );

                var jwtToken = new JwtSecurityToken(header, payload);
                var tokenHandler = new JwtSecurityTokenHandler();
                response.Token = tokenHandler.WriteToken(jwtToken);
                response.Roles = roles.ToList();
                response.Success = true;
                response.NombreCompleto = user.NombreCompleto; 
            }
            catch(SecurityException secEx)
            {
                _logger.LogWarning(secEx, "La autenticación falló para el usuario {UserName}", request.UserName);
                response.Success = false;
                response.ErrorMessage = secEx.Message;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error durante el proceso de logueo para el usuario {UserName}", request.UserName);
                response.ErrorMessage = ex.Message;
            }

            return response;
        }

        public async Task<BaseResponse> RegisterAsync(RegisterUserDto request)
        {
            var response = new BaseResponse();
            try
            {
                var identity = new ECommerceUserIdentity()
                {
                    UserName = request.Usuario,
                    NombreCompleto = request.NombreCompleto,
                    Email = request.Email,
                    FechaNacimiento = request.FechaNacimiento,
                    Direccion = request.Direccion
                };

                var result = await _userManager.CreateAsync(identity, request.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(identity, Constantes.RolCliente);
                    response.Success = true;
                }
                else
                {
                    response.Success = false;
                    response.ErrorMessage = string.Join("; ", result.Errors.Select(e=>e.Description));
                }

                response.Success = result.Succeeded;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "No se pudo registrar al usaurio";
                _logger.LogCritical(ex, "Error registrando al usuario {UserName}",request.Usuario);
            }
            return response;
        }
    }
}
