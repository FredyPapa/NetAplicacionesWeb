using ECommerceWeb.WebApi.DataAccess;
using ECommerceWeb.WebApi.Entities;
using ECommerceWeb.WebApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWeb.WebApi.Repositories.Services
{
    public class ClienteRepository : RepositoryBase<Cliente>, IClienteRepository
    {
        public ClienteRepository(ECommerceDbContext context) : base(context)
        {
        }

        public async Task<Cliente?> BuscarPorEmailAsync(string email)
        {
            return await _context.Set<Cliente>()
                .FirstOrDefaultAsync(p => p.Email == email);
        }
    }
}
