using FarmManagement.Dal.Entities;

namespace FarmManagement.Dal.Repositories.Infrastructure;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity> AddAsync(TEntity entity);

    Task<IReadOnlyList<TEntity>> GetAsync(Func<AnimalEntity, bool>? predicate = null);

    Task<TEntity?> GetByNameAsync(string name);

    Task<AnimalEntity> UpdateAsync(AnimalEntity entity, string name);

    Task<TEntity> RemoveAsync(TEntity entity);
}