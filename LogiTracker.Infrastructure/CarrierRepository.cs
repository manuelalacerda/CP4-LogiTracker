using LogiTracker.Application.DTOs;
using LogiTracker.Application.Services;
using LogiTracker.Infrastructure.Persistence;

namespace LogiTracker.Infrastructure;

/// <summary>
/// Repositório para operações de persistência e consulta de transportadoras.
/// </summary>
public sealed class CarrierRepository(ApplicationDbContext context) : ICarrierRepository
{
    /// <inheritdoc />
    public IReadOnlyList<CarrierResponse> GetAll()
    {
        return context.Carriers
            .OrderBy(c => c.Name)
            .Select(CarrierResponse.FromDomain)
            .ToList();
    }

    /// <inheritdoc />
    public CarrierResponse? GetById(Guid id)
    {
        var carrier = context.Carriers
            .FirstOrDefault(c => c.Id == id);

        return carrier is null ? null : CarrierResponse.FromDomain(carrier);
    }

    /// <inheritdoc />
    public CarrierResponse Create(CarrierRequest request)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var carrier = request.ToDomain();

        context.Carriers.Add(carrier);
        context.SaveChanges();

        return CarrierResponse.FromDomain(carrier);
    }

    /// <inheritdoc />
    public bool Delete(Guid id)
    {
        var carrier = context.Carriers.FirstOrDefault(c => c.Id == id);
        if (carrier is null)
            return false;

        context.Carriers.Remove(carrier);
        context.SaveChanges();

        return true;
    }
}
