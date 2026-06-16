using SystemDesign.Application.Common;
using SystemDesign.Domain.Entities;

namespace SystemDesign.Application.Diagrams;

public interface IDiagramRepository : IGenericRepository<Diagram>
{
    Task<IReadOnlyList<Diagram>> GetForUserAsync(
        string ownerId,
        string? search,
        CancellationToken cancellationToken);
}
