using LogiTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LogiTracker.Infrastructure.Persistence.Configurations;

/// <summary>
/// Configuração EF para a entidade Driver.
/// </summary>
public class DriverConfiguration : IEntityTypeConfiguration<Driver>
{
    public void Configure(EntityTypeBuilder<Driver> builder)
    {
        builder.ToTable("Drivers");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(d => d.LicenseNumber)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(d => d.CarrierId).IsRequired();

        builder.HasIndex(d => d.LicenseNumber)
            .IsUnique();

        builder.Property(d => d.Active).IsRequired();

        builder.Property(d => d.CreatedAt).IsRequired();

        // 1:N com Delivery
        builder.HasMany(d => d.Deliveries)
            .WithOne(del => del.Driver)
            .HasForeignKey(del => del.DriverId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
