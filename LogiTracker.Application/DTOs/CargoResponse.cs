using LogiTracker.Domain.Entities;

namespace LogiTracker.Application.DTOs;

/// <summary>
/// DTO de resposta para operações de carga.
/// </summary>
public record CargoResponse(Guid Id, string Description, int Weight, int MonetaryValue)
{
    public static CargoResponse FromDomain(Cargo cargo) =>
        new(cargo.Id, cargo.Description, cargo.Weight, cargo.MonetaryValue);
}
