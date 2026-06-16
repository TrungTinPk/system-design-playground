using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SystemDesign.Domain.Entities;

namespace SystemDesign.Infrastructure.Persistence.Configurations;

public sealed class DiagramConfiguration : IEntityTypeConfiguration<Diagram>
{
    public void Configure(EntityTypeBuilder<Diagram> builder)
    {
        builder.ToTable("Diagrams");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.OwnerId)
            .IsRequired()
            .HasMaxLength(128);

        // Owner-scoped listing always filters on OwnerId.
        builder.HasIndex(d => d.OwnerId);

        builder.Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(200);

        // Stored as nvarchar(max) for the serialized diagram payload.
        builder.Property(d => d.Content)
            .IsRequired();

        builder.Property(d => d.CreatedAtUtc)
            .IsRequired();

        builder.Property(d => d.UpdatedAtUtc)
            .IsRequired();
    }
}
