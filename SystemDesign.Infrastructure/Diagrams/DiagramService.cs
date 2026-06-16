using Microsoft.Extensions.Logging;
using SystemDesign.Application.Common;
using SystemDesign.Application.Diagrams;
using SystemDesign.Domain.Entities;

namespace SystemDesign.Infrastructure.Diagrams;

public sealed class DiagramService(
    IDiagramRepository repository,
    ILogger<DiagramService> logger) : IDiagramService
{
    public async Task<Result<DiagramDto>> CreateAsync(
        CreateDiagramRequest request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return Result.Failure<DiagramDto>("Diagram name is required.");
        }

        var now = DateTimeOffset.UtcNow;

        var diagram = new Diagram
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Content = request.Content,
            CreatedAtUtc = now,
            UpdatedAtUtc = now
        };

        await repository.AddAsync(diagram, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Created diagram {DiagramId}", diagram.Id);

        return Result.Success(ToDto(diagram));
    }

    public async Task<Result<DiagramDto>> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var diagram = await repository.GetByIdAsync(id, cancellationToken);

        return diagram is null
            ? Result.NotFound<DiagramDto>($"Diagram '{id}' was not found.")
            : Result.Success(ToDto(diagram));
    }

    public async Task<Result<IReadOnlyList<DiagramDto>>> GetForUserAsync(
        string ownerId,
        string? search,
        CancellationToken cancellationToken)
    {
        var diagrams = await repository.GetForUserAsync(ownerId, search, cancellationToken);

        IReadOnlyList<DiagramDto> dtos = diagrams.Select(ToDto).ToList();

        return Result.Success(dtos);
    }

    public async Task<Result<DiagramDto>> UpdateAsync(
        Guid id,
        UpdateDiagramRequest request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return Result.Failure<DiagramDto>("Diagram name is required.");
        }

        var diagram = await repository.GetByIdAsync(id, cancellationToken);

        if (diagram is null)
        {
            return Result.NotFound<DiagramDto>($"Diagram '{id}' was not found.");
        }

        diagram.Name = request.Name;
        diagram.Content = request.Content;
        diagram.UpdatedAtUtc = DateTimeOffset.UtcNow;

        repository.Update(diagram);
        await repository.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Updated diagram {DiagramId}", diagram.Id);

        return Result.Success(ToDto(diagram));
    }

    public async Task<Result> DeleteAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var diagram = await repository.GetByIdAsync(id, cancellationToken);

        if (diagram is null)
        {
            return Result.NotFound($"Diagram '{id}' was not found.");
        }

        repository.Remove(diagram);
        await repository.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Deleted diagram {DiagramId}", id);

        return Result.Success();
    }

    private static DiagramDto ToDto(Diagram diagram) =>
        new(
            diagram.Id,
            diagram.OwnerId,
            diagram.Name,
            diagram.Content,
            diagram.CreatedAtUtc,
            diagram.UpdatedAtUtc);
}
