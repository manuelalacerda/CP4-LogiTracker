using LogiTracker.Domain.Entities;

namespace LogiTracker.Application.DTOs;

/// <summary>
/// DTO de requisição para criação de entrega.
/// </summary>
public record DeliveryRequest(Guid VehicleId, Guid DriverId, Guid CargoId)
{
    public Delivery ToDomain() => new Delivery(VehicleId, DriverId, CargoId);
}
