using LogiTracker.Domain.Entities;

namespace LogiTracker.Application.DTOs;

/// <summary>
/// DTO de requisição para criação de transportadora.
/// </summary>
public record CarrierRequest(string Name, string TaxId)
{
    public Carrier ToDomain() => new Carrier(Name, TaxId);
}
