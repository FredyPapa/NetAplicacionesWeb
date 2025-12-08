using ECommerceWeb.WebApi.Entities;
using System.Linq.Expressions;

namespace ECommerceWeb.WebApi.Repositories.Interfaces
{
    public interface IRepositoryBase<TEntity>
        where TEntity : EntityBase
    {
        Task<ICollection<TEntity>> ListAsync();
        Task<ICollection<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate);

        Task<ICollection<TInfo>> ListAsync<TInfo>(Expression<Func<TEntity, bool>> predicate,
                Expression<Func<TEntity, TInfo>> selector);
        Task<int> AddAsync(TEntity entity);

        Task<TEntity?> GetByIdAsync(int id);

        Task UpdateAsync();

        Task DeleteAsync(int id);
    }
}
