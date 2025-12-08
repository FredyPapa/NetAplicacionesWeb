using ECommerceWeb.WebApi.DataAccess;
using ECommerceWeb.WebApi.Entities;
using ECommerceWeb.WebApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ECommerceWeb.WebApi.Repositories.Services
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
        where TEntity : EntityBase
    {
        protected readonly ECommerceDbContext _context;

        protected RepositoryBase(ECommerceDbContext context)
        {
            _context = context;
        }
        public async Task<ICollection<TEntity>> ListAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<ICollection<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>()
                .AsNoTracking()
                .Where(predicate)
                .ToListAsync();
        }

        public virtual async Task<ICollection<TInfo>> ListAsync<TInfo>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TInfo>> selector)
        {
            var query = _context.Set<TEntity>()
                .Where(predicate)
                .AsNoTracking()
                .Select(selector)
                .AsQueryable();

            return await query.ToListAsync();
        }

        public virtual async Task<int> AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            return await _context.SaveChangesAsync(); // Confirma los datos en la BD
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task UpdateAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity != null)
            {
                _context.Set<TEntity>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
