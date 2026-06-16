using SystemDesign.Domain.Common;

namespace SystemDesign.Domain.Entities;

public sealed class Diagram : IEntity
{
    public Guid Id { get; set; }

    /// <summary>
    /// Identifier of the user that owns the diagram (from the auth token).
    /// </summary>
    public string OwnerId { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Serialized diagram payload (e.g. React Flow nodes/edges as JSON).
    /// </summary>
    public string Content { get; set; } = string.Empty;

    public DateTimeOffset CreatedAtUtc { get; set; }

    public DateTimeOffset UpdatedAtUtc { get; set; }
}
