using ECommerceWeb.WebApi.DataAccess;
using ECommerceWeb.WebApi.Entities;
using ECommerceWeb.WebApi.Entities.Infos;
using ECommerceWeb.WebApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWeb.WebApi.Repositories.Services
{
    public class VentaRepository : RepositoryBase<Venta>, IVentaRepository
    {
        public VentaRepository(ECommerceDbContext context) : base(context)
        {
        }

        public override async Task<int> AddAsync(Venta entity)
        {
            await _context.AddAsync(entity);
            return entity.Id;
        }

        public async Task ConfirmarTransaccionAsync()
        {
            await _context.Database.CommitTransactionAsync();
        }

        public async Task CrearTransaccionAsync()
        {
            await _context.Database.BeginTransactionAsync();
        }

        public async Task ResetearTransaccionAsync()
        {
            await _context.Database.RollbackTransactionAsync();
        }

        public async Task<Dashboard> MostrarDashboard()
        {
            var entity = _context.Database.SqlQuery<Dashboard>(
                $"EXEC uspDashboard");
            return await Task.FromResult(entity.AsEnumerable().First());
        }

    }
}
