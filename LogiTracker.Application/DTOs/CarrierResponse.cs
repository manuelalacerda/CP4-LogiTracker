using LogiTracker.Domain.Entities;

namespace LogiTracker.Application.DTOs;

/// <summary>
/// DTO de resposta para operações de transportadora.
/// </summary>
public record CarrierResponse(Guid Id, string Name, string TaxId)
{
    public static CarrierResponse FromDomain(Carrier carrier) =>
        new(carrier.Id, carrier.Name, carrier.TaxId);
}
