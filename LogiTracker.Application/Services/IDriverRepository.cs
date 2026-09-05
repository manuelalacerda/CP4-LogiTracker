using LogiTracker.Application.DTOs;

namespace LogiTracker.Application.Services;

/// <summary>
/// Define as operações de repositório para o domínio de motoristas.
/// </summary>
public interface IDriverRepository
{
    /// <summary>
    /// Retorna todos os motoristas cadastrados.
    /// </summary>
    IReadOnlyList<DriverResponse> GetAll();

    /// <summary>
    /// Busca um motorista pelo identificador único.
    /// </summary>
    DriverResponse? GetById(Guid id);

    /// <summary>
    /// Cria um novo motorista a partir dos dados informados.
    /// </summary>
    DriverResponse Create(DriverRequest request);

    /// <summary>
    /// Remove um motorista pelo identificador único.
    /// </summary>
    bool Delete(Guid id);
}
