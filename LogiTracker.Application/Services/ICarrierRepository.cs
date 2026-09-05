using LogiTracker.Application.DTOs;

namespace LogiTracker.Application.Services;

/// <summary>
/// Define as operações de repositório para o domínio de transportadoras.
/// </summary>
public interface ICarrierRepository
{
    /// <summary>
    /// Retorna todas as transportadoras cadastradas.
    /// </summary>
    IReadOnlyList<CarrierResponse> GetAll();

    /// <summary>
    /// Busca uma transportadora pelo identificador único.
    /// </summary>
    CarrierResponse? GetById(Guid id);

    /// <summary>
    /// Cria uma nova transportadora a partir dos dados informados.
    /// </summary>
    CarrierResponse Create(CarrierRequest request);

    /// <summary>
    /// Remove uma transportadora pelo identificador único.
    /// </summary>
    bool Delete(Guid id);
}
