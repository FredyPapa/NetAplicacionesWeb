using Blazored.SessionStorage;
using ECommerceWeb.Common.Response;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace ECommerceWeb.WebApp.Auth
{
    public class AuthenticationService : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        private readonly ISessionStorageService _sessionStorageService;
        private readonly ClaimsPrincipal _anonimo = new ClaimsPrincipal(new ClaimsIdentity());

        public AuthenticationService(HttpClient httpClient, ISessionStorageService sessionStorageService)
        {
            _httpClient = httpClient;
            _sessionStorageService = sessionStorageService;
        }

        public async Task Autenticar(LoginDtoResponse? response)
        {
            //Esto representa la identidad del usuario
            ClaimsPrincipal claimsPrincipal;
            if(response is not null)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", response.Token);

                var jwt = ParseToken(response);

                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(jwt.Claims, "JWT"));

                //Guardamos la sesión
                await _sessionStorageService.SetItemAsync("session", response);
            }
            else
            {
                _httpClient.DefaultRequestHeaders.Authorization = null;
                claimsPrincipal = _anonimo;

                //Eliminamos la sesión
                await _sessionStorageService.RemoveItemAsync("session");
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        private JwtSecurityToken ParseToken(LoginDtoResponse response)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(response.Token);
            return token;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var sesionUsuario = await _sessionStorageService.GetItemAsync<LoginDtoResponse>("session");

            if(sesionUsuario is null)
            {
                return await Task.FromResult(new AuthenticationState(_anonimo));
            }

            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(ParseToken(sesionUsuario).Claims, "JWT"));

            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }
    }
}
