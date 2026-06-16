namespace SystemDesign.Domain.Common;

/// <summary>
/// Marker for entities that use a <see cref="Guid"/> primary key and can be
/// served by the generic repository.
/// </summary>
public interface IEntity
{
    Guid Id { get; }
}
