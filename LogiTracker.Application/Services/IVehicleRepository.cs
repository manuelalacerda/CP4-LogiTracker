using LogiTracker.Application.DTOs;

namespace LogiTracker.Application.Services;

/// <summary>
/// Define as operações de repositório para o domínio de veículos.
/// </summary>
public interface IVehicleRepository
{
    /// <summary>
    /// Retorna todos os veículos cadastrados.
    /// </summary>
    IReadOnlyList<VehicleResponse> GetAll();

    /// <summary>
    /// Busca um veículo pelo identificador único.
    /// </summary>
    VehicleResponse? GetById(Guid id);

    /// <summary>
    /// Cria um novo veículo a partir dos dados informados.
    /// </summary>
    VehicleResponse Create(VehicleRequest request);

    /// <summary>
    /// Remove um veículo pelo identificador único.
    /// </summary>
    bool Delete(Guid id);
}
