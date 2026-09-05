using LogiTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LogiTracker.Infrastructure.Persistence.Configurations;

/// <summary>
/// Configuração EF para a entidade Cargo.
/// </summary>
public class CargoConfiguration : IEntityTypeConfiguration<Cargo>
{
    public void Configure(EntityTypeBuilder<Cargo> builder)
    {
        builder.ToTable("Cargos");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Description)
            .HasMaxLength(500)
            .IsRequired();

        // Peso em gramas (int)
        builder.Property(c => c.Weight).IsRequired();

        // Valor monetário em centavos (int)
        builder.Property(c => c.MonetaryValue).IsRequired();

        builder.Property(c => c.Active).IsRequired();

        builder.Property(c => c.CreatedAt).IsRequired();

        // 1:1 com Delivery — FK fica em Deliveries
        builder.HasOne(c => c.Delivery)
            .WithOne(d => d.Cargo)
            .HasForeignKey<Delivery>(d => d.CargoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
