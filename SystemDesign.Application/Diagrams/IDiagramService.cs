using SystemDesign.Application.Common;

namespace SystemDesign.Application.Diagrams;

public interface IDiagramService
{
    Task<Result<DiagramDto>> CreateAsync(
        CreateDiagramRequest request,
        CancellationToken cancellationToken);

    Task<Result<DiagramDto>> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<Result<IReadOnlyList<DiagramDto>>> GetForUserAsync(
        string ownerId,
        string? search,
        CancellationToken cancellationToken);

    Task<Result<DiagramDto>> UpdateAsync(
        Guid id,
        UpdateDiagramRequest request,
        CancellationToken cancellationToken);

    Task<Result> DeleteAsync(
        Guid id,
        CancellationToken cancellationToken);
}
