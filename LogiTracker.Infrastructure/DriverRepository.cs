using LogiTracker.Application.DTOs;
using LogiTracker.Application.Services;
using LogiTracker.Infrastructure.Persistence;

namespace LogiTracker.Infrastructure;

/// <summary>
/// Repositório para operações de persistência e consulta de motoristas.
/// </summary>
public sealed class DriverRepository(ApplicationDbContext context) : IDriverRepository
{
    /// <inheritdoc />
    public IReadOnlyList<DriverResponse> GetAll()
    {
        return context.Drivers
            .OrderBy(d => d.Name)
            .Select(DriverResponse.FromDomain)
            .ToList();
    }

    /// <inheritdoc />
    public DriverResponse? GetById(Guid id)
    {
        var driver = context.Drivers
            .FirstOrDefault(d => d.Id == id);

        return driver is null ? null : DriverResponse.FromDomain(driver);
    }

    /// <inheritdoc />
    public DriverResponse Create(DriverRequest request)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var driver = request.ToDomain();

        context.Drivers.Add(driver);
        context.SaveChanges();

        return DriverResponse.FromDomain(driver);
    }

    /// <inheritdoc />
    public bool Delete(Guid id)
    {
        var driver = context.Drivers.FirstOrDefault(d => d.Id == id);
        if (driver is null)
            return false;

        context.Drivers.Remove(driver);
        context.SaveChanges();

        return true;
    }
}
