using LogiTracker.Domain.Entities;
using LogiTracker.Domain.Enums;

namespace LogiTracker.Application.DTOs;

/// <summary>
/// DTO de resposta para operações de entrega.
/// </summary>
public record DeliveryResponse(Guid Id, DeliveryStatus Status, DateTime OrderDate, Guid VehicleId, Guid DriverId, Guid CargoId)
{
    public static DeliveryResponse FromDomain(Delivery delivery) =>
        new(delivery.Id, delivery.Status, delivery.OrderDate, delivery.VehicleId, delivery.DriverId, delivery.CargoId);
}
