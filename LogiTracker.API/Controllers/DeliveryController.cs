using LogiTracker.Application.DTOs;
using LogiTracker.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace LogiTracker.API.Controllers;

/// <summary>
/// Controller responsável pelas operações de entregas na API.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class DeliveryController : ControllerBase
{
    private readonly IDeliveryRepository _deliveryRepository;
    private readonly IDeliveryService _deliveryService;
    private readonly ILogger<DeliveryController> _logger;

    public DeliveryController(
        IDeliveryRepository deliveryRepository,
        IDeliveryService deliveryService,
        ILogger<DeliveryController> logger)
    {
        _deliveryRepository = deliveryRepository;
        _deliveryService = deliveryService;
        _logger = logger;
    }

    /// <summary>
    /// Lista todas as entregas cadastradas.
    /// </summary>
    /// <response code="200">
    /// Retorna todas as entregas cadastradas.
    /// </response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        var deliveries = _deliveryRepository.GetAll();

        return Ok(deliveries);
    }

    /// <summary>
    /// Busca uma entrega pelo identificador único.
    /// </summary>
    /// <param name="id">Identificador único da entrega.</param>
    /// <response code="200">
    /// Retorna a entrega encontrada.
    /// </response>
    /// <response code="404">
    /// Entrega não encontrada.
    /// </response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(Guid id)
    {
        var delivery = _deliveryRepository.GetById(id);

        if (delivery is null)
            throw new KeyNotFoundException("Entrega não encontrada.");

        return Ok(delivery);
    }

    /// <summary>
    /// Cria uma nova entrega.
    /// </summary>
    /// <param name="request">Dados da entrega.</param>
    /// <response code="201">
    /// Entrega criada com sucesso.
    /// </response>
    /// <response code="400">
    /// Dados inválidos.
    /// </response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create([FromBody] DeliveryRequest request)
    {
        var traceId = HttpContext.TraceIdentifier;

        _logger.LogInformation(
            "Iniciando criação de entrega. VehicleId: {VehicleId}, DriverId: {DriverId}, CargoId: {CargoId}, TraceId: {TraceId}",
            request.VehicleId,
            request.DriverId,
            request.CargoId,
            traceId);

        var delivery = await _deliveryService.CreateAsync(request);

        _logger.LogInformation(
            "Entrega criada com sucesso. DeliveryId: {DeliveryId}, TraceId: {TraceId}",
            delivery.Id,
            traceId);

        return CreatedAtAction(
            nameof(GetById),
            new { id = delivery.Id },
            delivery
        );
    }

    /// <summary>
    /// Remove uma entrega pelo identificador único.
    /// </summary>
    /// <param name="id">Identificador único da entrega.</param>
    /// <response code="204">
    /// Entrega removida com sucesso.
    /// </response>
    /// <response code="404">
    /// Entrega não encontrada.
    /// </response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(Guid id)
    {
        var deleted = _deliveryRepository.Delete(id);

        if (!deleted)
            throw new KeyNotFoundException("Entrega não encontrada.");

        return NoContent();
    }
}