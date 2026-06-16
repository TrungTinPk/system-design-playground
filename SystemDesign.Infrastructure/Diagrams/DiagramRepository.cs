using Microsoft.EntityFrameworkCore;
using SystemDesign.Application.Diagrams;
using SystemDesign.Domain.Entities;
using SystemDesign.Infrastructure.Persistence;

namespace SystemDesign.Infrastructure.Diagrams;

public sealed class DiagramRepository(ApplicationDbContext dbContext)
    : GenericRepository<Diagram>(dbContext), IDiagramRepository
{
    public async Task<IReadOnlyList<Diagram>> GetForUserAsync(
        string ownerId,
        string? search,
        CancellationToken cancellationToken)
    {
        // Ownership is enforced in the query, never filtered in memory afterwards.
        var query = Set.Where(d => d.OwnerId == ownerId);

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(d => d.Name.Contains(search));
        }

        return await query
            .OrderByDescending(d => d.UpdatedAtUtc)
            .ToListAsync(cancellationToken);
    }
}
