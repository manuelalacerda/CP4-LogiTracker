using LogiTracker.Domain.Entities;
using LogiTracker.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LogiTracker.Infrastructure.Persistence.Configurations;

/// <summary>
/// Configuração EF para a entidade Delivery.
/// </summary>
public class DeliveryConfiguration : IEntityTypeConfiguration<Delivery>
{
    public void Configure(EntityTypeBuilder<Delivery> builder)
    {
        builder.ToTable("Deliveries");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.Status)
            .IsRequired()
            .HasConversion<int>()
            .HasDefaultValue(DeliveryStatus.Pending);

        builder.Property(d => d.OrderDate).IsRequired();

        builder.Property(d => d.VehicleId).IsRequired();

        builder.Property(d => d.DriverId).IsRequired();

        builder.Property(d => d.CargoId).IsRequired();

        // Índice único em CargoId garante a cardinalidade 1:1 com Cargo
        builder.HasIndex(d => d.CargoId)
            .IsUnique();

        builder.Property(d => d.Active).IsRequired();

        builder.Property(d => d.CreatedAt).IsRequired();

        // N:1 com Vehicle
        builder.HasOne(d => d.Vehicle)
            .WithMany(v => v.Deliveries)
            .HasForeignKey(d => d.VehicleId)
            .OnDelete(DeleteBehavior.Restrict);

        // N:1 com Driver
        builder.HasOne(d => d.Driver)
            .WithMany(dr => dr.Deliveries)
            .HasForeignKey(d => d.DriverId)
            .OnDelete(DeleteBehavior.Restrict);

        // 1:1 com Cargo
        builder.HasOne(d => d.Cargo)
            .WithOne(c => c.Delivery)
            .HasForeignKey<Delivery>(d => d.CargoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
