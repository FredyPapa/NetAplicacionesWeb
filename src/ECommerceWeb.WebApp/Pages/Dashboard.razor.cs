using ECommerceWeb.Common.Response;
using System.Net.Http.Json;

namespace ECommerceWeb.WebApp.Pages
{
    public partial class Dashboard
    {

        public DashboardDto DashboardDto { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var response = await HttpClient.GetFromJsonAsync<BaseResponse<DashboardDto>>("api/ventas/dashboard");
                if (response is { Data: not null, Success: true })
                {
                    DashboardDto = response.Data;
                }
                else
                {
                    ToastService.ShowError("No se pudo cargar la información del dashboard");
                }
            }
            catch (Exception ex)
            {
                ToastService.ShowError(ex.Message);
            }

        }

    }
}