using LogiTracker.Application.DTOs;
using LogiTracker.Application.Services;
using LogiTracker.Infrastructure.Persistence;

namespace LogiTracker.Infrastructure;

/// <summary>
/// Repositório para operações de persistência e consulta de entregas.
/// </summary>
public sealed class DeliveryRepository(ApplicationDbContext context) : IDeliveryRepository
{
    /// <inheritdoc />
    public IReadOnlyList<DeliveryResponse> GetAll()
    {
        return context.Deliveries
            .OrderBy(d => d.OrderDate)
            .Select(DeliveryResponse.FromDomain)
            .ToList();
    }

    /// <inheritdoc />
    public DeliveryResponse? GetById(Guid id)
    {
        var delivery = context.Deliveries
            .FirstOrDefault(d => d.Id == id);

        return delivery is null ? null : DeliveryResponse.FromDomain(delivery);
    }

    /// <inheritdoc />
    public DeliveryResponse Create(DeliveryRequest request)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var delivery = request.ToDomain();

        context.Deliveries.Add(delivery);
        context.SaveChanges();

        return DeliveryResponse.FromDomain(delivery);
    }

    /// <inheritdoc />
    public bool Delete(Guid id)
    {
        var delivery = context.Deliveries.FirstOrDefault(d => d.Id == id);
        if (delivery is null)
            return false;

        context.Deliveries.Remove(delivery);
        context.SaveChanges();

        return true;
    }
}
