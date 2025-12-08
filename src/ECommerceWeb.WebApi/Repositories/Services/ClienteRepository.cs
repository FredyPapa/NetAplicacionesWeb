using ECommerceWeb.WebApi.DataAccess;
using ECommerceWeb.WebApi.Entities;
using ECommerceWeb.WebApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

        public async Task<ICollection<Cliente>> ListarConTipoClienteAsync(Expression<Func<Cliente, bool>> predicate)
        {
            return await _context.Set<Cliente>()
                .Include(c => c.TipoCliente)
                .Where(predicate)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
