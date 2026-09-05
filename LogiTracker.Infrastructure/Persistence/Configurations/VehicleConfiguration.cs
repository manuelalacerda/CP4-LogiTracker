using LogiTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LogiTracker.Infrastructure.Persistence.Configurations;

/// <summary>
/// Configuração EF para a entidade Vehicle.
/// </summary>
public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.ToTable("Vehicles");

        builder.HasKey(v => v.Id);

        builder.Property(v => v.Plate)
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(v => v.Model)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(v => v.CarrierId).IsRequired();

        builder.HasIndex(v => v.Plate)
            .IsUnique();

        builder.Property(v => v.Active).IsRequired();

        builder.Property(v => v.CreatedAt).IsRequired();

        // 1:N com Delivery
        builder.HasMany(v => v.Deliveries)
            .WithOne(d => d.Vehicle)
            .HasForeignKey(d => d.VehicleId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
