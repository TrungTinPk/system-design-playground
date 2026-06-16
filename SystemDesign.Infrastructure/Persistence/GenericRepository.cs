using Microsoft.EntityFrameworkCore;
using SystemDesign.Application.Common;
using SystemDesign.Domain.Common;

namespace SystemDesign.Infrastructure.Persistence;

public class GenericRepository<TEntity>(ApplicationDbContext dbContext)
    : IGenericRepository<TEntity>
    where TEntity : class, IEntity
{
    protected ApplicationDbContext DbContext { get; } = dbContext;

    protected DbSet<TEntity> Set => DbContext.Set<TEntity>();

    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        await Set.FindAsync([id], cancellationToken);

    public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken) =>
        await Set.ToListAsync(cancellationToken);

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken) =>
        await Set.AddAsync(entity, cancellationToken);

    public void Update(TEntity entity) =>
        Set.Update(entity);

    public void Remove(TEntity entity) =>
        Set.Remove(entity);

    public async Task SaveChangesAsync(CancellationToken cancellationToken) =>
        await DbContext.SaveChangesAsync(cancellationToken);
}
