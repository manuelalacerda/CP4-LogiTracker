using LogiTracker.Application.DTOs;
using LogiTracker.Application.Services;
using LogiTracker.Infrastructure.Persistence;

namespace LogiTracker.Infrastructure;

/// <summary>
/// Repositório para operações de persistência e consulta de veículos.
/// </summary>
public sealed class VehicleRepository(ApplicationDbContext context) : IVehicleRepository
{
    /// <inheritdoc />
    public IReadOnlyList<VehicleResponse> GetAll()
    {
        return context.Vehicles
            .OrderBy(v => v.Plate)
            .Select(VehicleResponse.FromDomain)
            .ToList();
    }

    /// <inheritdoc />
    public VehicleResponse? GetById(Guid id)
    {
        var vehicle = context.Vehicles
            .FirstOrDefault(v => v.Id == id);

        return vehicle is null ? null : VehicleResponse.FromDomain(vehicle);
    }

    /// <inheritdoc />
    public VehicleResponse Create(VehicleRequest request)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var vehicle = request.ToDomain();

        context.Vehicles.Add(vehicle);
        context.SaveChanges();

        return VehicleResponse.FromDomain(vehicle);
    }

    /// <inheritdoc />
    public bool Delete(Guid id)
    {
        var vehicle = context.Vehicles.FirstOrDefault(v => v.Id == id);
        if (vehicle is null)
            return false;

        context.Vehicles.Remove(vehicle);
        context.SaveChanges();

        return true;
    }
}
