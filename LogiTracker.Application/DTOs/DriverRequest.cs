using LogiTracker.Domain.Entities;

namespace LogiTracker.Application.DTOs;

/// <summary>
/// DTO de requisição para criação de motorista.
/// </summary>
public record DriverRequest(string Name, string LicenseNumber, Guid CarrierId)
{
    public Driver ToDomain() => new Driver(Name, LicenseNumber, CarrierId);
}
