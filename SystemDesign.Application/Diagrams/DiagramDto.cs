namespace SystemDesign.Application.Diagrams;

public sealed record DiagramDto(
    Guid Id,
    string OwnerId,
    string Name,
    string Content,
    DateTimeOffset CreatedAtUtc,
    DateTimeOffset UpdatedAtUtc);
