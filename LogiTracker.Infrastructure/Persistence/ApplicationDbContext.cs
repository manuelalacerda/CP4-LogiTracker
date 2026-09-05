using LogiTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LogiTracker.Infrastructure.Persistence;

/// <summary>
/// Contexto EF Core do projeto LogiTracker.
/// Configura o mapeamento das entidades para o banco de dados.
/// </summary>
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Carrier> Carriers { get; set; }

    public DbSet<Vehicle> Vehicles { get; set; }

    public DbSet<Driver> Drivers { get; set; }

    public DbSet<Cargo> Cargos { get; set; }

    public DbSet<Delivery> Deliveries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
