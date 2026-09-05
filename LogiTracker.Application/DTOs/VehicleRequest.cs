using LogiTracker.Domain.Entities;

namespace LogiTracker.Application.DTOs;

/// <summary>
/// DTO de requisição para criação de veículo.
/// </summary>
public record VehicleRequest(string Plate, string Model, Guid CarrierId)
{
    public Vehicle ToDomain() => new Vehicle(Plate, Model, CarrierId);
}
