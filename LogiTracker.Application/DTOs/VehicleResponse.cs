using LogiTracker.Domain.Entities;

namespace LogiTracker.Application.DTOs;

/// <summary>
/// DTO de resposta para operações de veículo.
/// </summary>
public record VehicleResponse(Guid Id, string Plate, string Model, Guid CarrierId)
{
    public static VehicleResponse FromDomain(Vehicle vehicle) =>
        new(vehicle.Id, vehicle.Plate, vehicle.Model, vehicle.CarrierId);
}
