using ECommerceWeb.WebApi.DataAccess;
using ECommerceWeb.WebApi.Entities;
using ECommerceWeb.WebApi.Repositories.Interfaces;

namespace ECommerceWeb.WebApi.Repositories.Services
{
    public class MarcaRepository : RepositoryBase<Marca>, IMarcaRepository
    {
        public MarcaRepository(ECommerceDbContext context) : base(context)
        {
        }
    }
}
