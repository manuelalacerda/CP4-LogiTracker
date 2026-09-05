using LogiTracker.Domain.Entities;

namespace LogiTracker.Application.DTOs;

/// <summary>
/// DTO de requisição para criação de carga.
/// </summary>
public record CargoRequest(string Description, int Weight, int MonetaryValue)
{
    public Cargo ToDomain() => new Cargo(Description, Weight, MonetaryValue);
}
