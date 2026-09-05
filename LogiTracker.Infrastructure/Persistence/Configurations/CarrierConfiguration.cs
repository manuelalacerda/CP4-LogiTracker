using LogiTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LogiTracker.Infrastructure.Persistence.Configurations;

/// <summary>
/// Configuração EF para a entidade Carrier.
/// </summary>
public class CarrierConfiguration : IEntityTypeConfiguration<Carrier>
{
    public void Configure(EntityTypeBuilder<Carrier> builder)
    {
        builder.ToTable("Carriers");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(c => c.TaxId)
            .HasMaxLength(18)
            .IsRequired();

        builder.HasIndex(c => c.TaxId)
            .IsUnique();

        builder.Property(c => c.Active).IsRequired();

        builder.Property(c => c.CreatedAt).IsRequired();

        // 1:N com Vehicle
        builder.HasMany(c => c.Vehicles)
            .WithOne(v => v.Carrier)
            .HasForeignKey(v => v.CarrierId)
            .OnDelete(DeleteBehavior.Restrict);

        // 1:N com Driver
        builder.HasMany(c => c.Drivers)
            .WithOne(d => d.Carrier)
            .HasForeignKey(d => d.CarrierId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
