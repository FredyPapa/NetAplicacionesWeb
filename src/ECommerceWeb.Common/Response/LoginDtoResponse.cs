using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceWeb.Common.Response
{
    public class LoginDtoResponse:BaseResponse
    {
        public string Token { get; set; } = null!;
        public string NombreCompleto { get; set; } = null!;
        public List<string> Roles { get; set; } = null!;
    }
}
