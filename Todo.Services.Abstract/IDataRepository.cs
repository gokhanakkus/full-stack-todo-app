using Todo.Shared.Data;

namespace Todo.Services.Abstract
{
    public interface IDataRepository<TEntity> where TEntity : class, IEntity<long>
    {
        IQueryable<TEntity> GetAll();
        Task<TEntity?> GetByIdAsync(long id);
        Task<TEntity> AddAsync(TEntity entity, long userId);
        Task<TEntity?> UpdateAsync(long id, TEntity entity, long userId);
        Task<bool> DeleteAsync(long id);
    }
}
