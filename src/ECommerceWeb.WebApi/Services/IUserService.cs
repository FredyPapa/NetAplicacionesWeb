using ECommerceWeb.Common.Request;
using ECommerceWeb.Common.Response;

namespace ECommerceWeb.WebApi.Services
{
    public interface IUserService
    {
        Task<LoginDtoResponse> LoginAsync(LoginDtoRequest request);

        Task<BaseResponse> RegisterAsync(RegisterUserDto request);
    }
}
