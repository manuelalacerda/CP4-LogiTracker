using LogiTracker.Application.DTOs;
using LogiTracker.Application.Services;
using LogiTracker.Infrastructure.Persistence;

namespace LogiTracker.Infrastructure;

/// <summary>
/// Repositório para operações de persistência e consulta de cargas.
/// </summary>
public sealed class CargoRepository(ApplicationDbContext context) : ICargoRepository
{
    /// <inheritdoc />
    public IReadOnlyList<CargoResponse> GetAll()
    {
        return context.Cargos
            .OrderBy(c => c.Description)
            .Select(CargoResponse.FromDomain)
            .ToList();
    }

    /// <inheritdoc />
    public CargoResponse? GetById(Guid id)
    {
        var cargo = context.Cargos
            .FirstOrDefault(c => c.Id == id);

        return cargo is null ? null : CargoResponse.FromDomain(cargo);
    }

    /// <inheritdoc />
    public CargoResponse Create(CargoRequest request)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var cargo = request.ToDomain();

        context.Cargos.Add(cargo);
        context.SaveChanges();

        return CargoResponse.FromDomain(cargo);
    }

    /// <inheritdoc />
    public bool Delete(Guid id)
    {
        var cargo = context.Cargos.FirstOrDefault(c => c.Id == id);
        if (cargo is null)
            return false;

        context.Cargos.Remove(cargo);
        context.SaveChanges();

        return true;
    }
}
