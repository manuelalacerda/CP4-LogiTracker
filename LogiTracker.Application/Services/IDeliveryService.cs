using LogiTracker.Application.DTOs;

namespace LogiTracker.Application.Services;

/// <summary>
/// Orquestra o caso de uso de criação de entregas, validando as dependências
/// (veículo, motorista e carga) antes de delegar a persistência ao
/// <see cref="IDeliveryRepository"/>.
/// </summary>
public interface IDeliveryService
{
    /// <summary>
    /// Cria uma entrega após validar que o veículo, o motorista e a carga informados existem.
    /// </summary>
    /// <exception cref="KeyNotFoundException">
    /// Lançada quando o veículo, o motorista ou a carga informados não existem.
    /// Nesse caso, nenhuma entrega é persistida.
    /// </exception>
    Task<DeliveryResponse> CreateAsync(DeliveryRequest request);
}
