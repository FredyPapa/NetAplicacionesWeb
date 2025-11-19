using ECommerceWeb.Common.Request;
using ECommerceWeb.Common.Response;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ECommerceWeb.WebApi.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;

        public UserService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<LoginDtoResponse> LoginAsync(LoginDtoRequest request)
        {
            var response = new LoginDtoResponse();

            if(request.UserName == "admin" &&  request.Password == "admin")
            {
                //Generar token
                var llaveSimetrica = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));
                var credenciales = new SigningCredentials(llaveSimetrica,SecurityAlgorithms.HmacSha256);

                var header = new JwtHeader(credenciales);
                var payload = new JwtPayload(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims:null,
                    notBefore:DateTime.UtcNow,
                    expires:DateTime.UtcNow.AddHours(1)
                );

                var jwtToken = new JwtSecurityToken(header, payload);
                var tokenHandler = new JwtSecurityTokenHandler();
                response.Token = tokenHandler.WriteToken(jwtToken);

                response.Success = true;
                response.NombreCompleto = "Administrador del Sistema";
            }
            else
            {
                response.ErrorMessage = "Usuario y/o clave no válidos";
            }

            return await Task.FromResult(response);
        }
    }
}
