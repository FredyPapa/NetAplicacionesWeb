using ECommerceWeb.Common.Response;
using Microsoft.AspNetCore.Components;

namespace ECommerceWeb.WebApp.Pages
{
    public partial class ProductCard
    {
        [Parameter]
        public ProductoDtoResponse Producto { get; set; } = new ProductoDtoResponse();

        [Parameter]
        public EventCallback<int> OnClick { get; set; }
    }
}