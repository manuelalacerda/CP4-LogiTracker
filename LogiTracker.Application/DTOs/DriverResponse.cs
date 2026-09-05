using LogiTracker.Domain.Entities;

namespace LogiTracker.Application.DTOs;

/// <summary>
/// DTO de resposta para operações de motorista.
/// </summary>
public record DriverResponse(Guid Id, string Name, string LicenseNumber, Guid CarrierId)
{
    public static DriverResponse FromDomain(Driver driver) =>
        new(driver.Id, driver.Name, driver.LicenseNumber, driver.CarrierId);
}
