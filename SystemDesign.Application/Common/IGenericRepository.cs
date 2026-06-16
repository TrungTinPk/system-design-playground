using SystemDesign.Domain.Common;

namespace SystemDesign.Application.Common;

public interface IGenericRepository<TEntity>
    where TEntity : class, IEntity
{
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken);

    Task AddAsync(TEntity entity, CancellationToken cancellationToken);

    void Update(TEntity entity);

    void Remove(TEntity entity);

    Task SaveChangesAsync(CancellationToken cancellationToken);
}
