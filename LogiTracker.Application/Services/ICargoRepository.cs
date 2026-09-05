using LogiTracker.Application.DTOs;

namespace LogiTracker.Application.Services;

/// <summary>
/// Define as operações de repositório para o domínio de cargas.
/// </summary>
public interface ICargoRepository
{
    /// <summary>
    /// Retorna todas as cargas cadastradas.
    /// </summary>
    IReadOnlyList<CargoResponse> GetAll();

    /// <summary>
    /// Busca uma carga pelo identificador único.
    /// </summary>
    CargoResponse? GetById(Guid id);

    /// <summary>
    /// Cria uma nova carga a partir dos dados informados.
    /// </summary>
    CargoResponse Create(CargoRequest request);

    /// <summary>
    /// Remove uma carga pelo identificador único.
    /// </summary>
    bool Delete(Guid id);
}
