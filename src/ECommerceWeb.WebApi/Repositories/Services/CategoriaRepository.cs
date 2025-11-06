using ECommerceWeb.WebApi.DataAccess;
using ECommerceWeb.WebApi.Entities;
using ECommerceWeb.WebApi.Repositories.Interfaces;

namespace ECommerceWeb.WebApi.Repositories.Services
{
    public class CategoriaRepository : RepositoryBase<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(ECommerceDbContext context) : base(context)
        {
        }
    }
}
