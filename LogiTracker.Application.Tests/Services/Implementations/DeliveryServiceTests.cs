using LogiTracker.Application.DTOs;
using LogiTracker.Application.Services;
using LogiTracker.Application.Services.Implementations;
using LogiTracker.Domain.Entities;
using LogiTracker.Domain.Enums;
using Moq;

namespace LogiTracker.Application.Tests.Services.Implementations;

public class DeliveryServiceTests
{
    private readonly Mock<IRepository<Vehicle>> _vehicleRepository = new();
    private readonly Mock<IRepository<Driver>> _driverRepository = new();
    private readonly Mock<IRepository<Cargo>> _cargoRepository = new();
    private readonly Mock<IDeliveryRepository> _deliveryRepository = new();
    private readonly DeliveryService _deliveryService;

    public DeliveryServiceTests()
    {
        _deliveryService = new DeliveryService(
            _vehicleRepository.Object,
            _driverRepository.Object,
            _cargoRepository.Object,
            _deliveryRepository.Object);
    }

    [Fact]
    public async Task CreateAsync_QuandoVeiculoNaoExiste_DeveLancarExceptionENaoPersistir()
    {
        // Arrange
        var request = new DeliveryRequest(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

        _vehicleRepository.Setup(r => r.ExistsByIdAsync(request.VehicleId)).ReturnsAsync(false);
        _driverRepository.Setup(r => r.ExistsByIdAsync(request.DriverId)).ReturnsAsync(true);
        _cargoRepository.Setup(r => r.ExistsByIdAsync(request.CargoId)).ReturnsAsync(true);

        // Act
        var act = () => _deliveryService.CreateAsync(request);

        // Assert
        var ex = await Assert.ThrowsAsync<KeyNotFoundException>(act);
        Assert.Equal("Veículo não encontrado.", ex.Message);
        _deliveryRepository.Verify(r => r.Create(It.IsAny<DeliveryRequest>()), Times.Never);
    }

    [Fact]
    public async Task CreateAsync_QuandoMotoristaNaoExiste_DeveLancarExceptionENaoPersistir()
    {
        // Arrange
        var request = new DeliveryRequest(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

        _vehicleRepository.Setup(r => r.ExistsByIdAsync(request.VehicleId)).ReturnsAsync(true);
        _driverRepository.Setup(r => r.ExistsByIdAsync(request.DriverId)).ReturnsAsync(false);
        _cargoRepository.Setup(r => r.ExistsByIdAsync(request.CargoId)).ReturnsAsync(true);

        // Act
        var act = () => _deliveryService.CreateAsync(request);

        // Assert
        var ex = await Assert.ThrowsAsync<KeyNotFoundException>(act);
        Assert.Equal("Motorista não encontrado.", ex.Message);
        _deliveryRepository.Verify(r => r.Create(It.IsAny<DeliveryRequest>()), Times.Never);
    }

    [Fact]
    public async Task CreateAsync_QuandoCargaNaoExiste_DeveLancarExceptionENaoPersistir()
    {
        // Arrange
        var request = new DeliveryRequest(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

        _vehicleRepository.Setup(r => r.ExistsByIdAsync(request.VehicleId)).ReturnsAsync(true);
        _driverRepository.Setup(r => r.ExistsByIdAsync(request.DriverId)).ReturnsAsync(true);
        _cargoRepository.Setup(r => r.ExistsByIdAsync(request.CargoId)).ReturnsAsync(false);

        // Act
        var act = () => _deliveryService.CreateAsync(request);

        // Assert
        var ex = await Assert.ThrowsAsync<KeyNotFoundException>(act);
        Assert.Equal("Carga não encontrada.", ex.Message);
        _deliveryRepository.Verify(r => r.Create(It.IsAny<DeliveryRequest>()), Times.Never);
    }

    [Fact]
    public async Task CreateAsync_ComTodasAsDependenciasExistentes_DevePersistirUmaVez()
    {
        // Arrange
        var request = new DeliveryRequest(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

        _vehicleRepository.Setup(r => r.ExistsByIdAsync(request.VehicleId)).ReturnsAsync(true);
        _driverRepository.Setup(r => r.ExistsByIdAsync(request.DriverId)).ReturnsAsync(true);
        _cargoRepository.Setup(r => r.ExistsByIdAsync(request.CargoId)).ReturnsAsync(true);

        var expectedResponse = new DeliveryResponse(
            Guid.NewGuid(),
            DeliveryStatus.Pending,
            DateTime.UtcNow,
            request.VehicleId,
            request.DriverId,
            request.CargoId);

        _deliveryRepository.Setup(r => r.Create(request)).Returns(expectedResponse);

        // Act
        var response = await _deliveryService.CreateAsync(request);

        // Assert
        Assert.Equal(expectedResponse.Id, response.Id);
        _deliveryRepository.Verify(r => r.Create(request), Times.Once);
    }
}
