using ECommerceWeb.WebApi.DataAccess;
using ECommerceWeb.WebApi.Entities;
using ECommerceWeb.WebApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWeb.WebApi.Repositories.Services
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
        where TEntity : EntityBase
    {
        private readonly ECommerceDbContext _context;

        protected RepositoryBase(ECommerceDbContext context)
        {
            _context = context;
        }
        public async Task<ICollection<TEntity>> ListAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }
        public async Task<int> AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            return await _context.SaveChangesAsync();
        }
    }
}
