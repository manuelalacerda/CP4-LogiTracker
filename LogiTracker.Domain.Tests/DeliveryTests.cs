using LogiTracker.Domain.Entities;
using LogiTracker.Domain.Enums;

namespace LogiTracker.Domain.Tests;

public class DeliveryTests
{
    [Fact]
    public void Construtor_ComDadosValidos_DeveCriarEntregaPendente()
    {
        // Arrange
        var vehicleId = Guid.NewGuid();
        var driverId = Guid.NewGuid();
        var cargoId = Guid.NewGuid();

        // Act
        var delivery = new Delivery(vehicleId, driverId, cargoId);

        // Assert
        Assert.Equal(DeliveryStatus.Pending, delivery.Status);
        Assert.Equal(vehicleId, delivery.VehicleId);
        Assert.Equal(driverId, delivery.DriverId);
        Assert.Equal(cargoId, delivery.CargoId);
        Assert.True(delivery.Active);
    }

    [Fact]
    public void ChangeStatus_DePendingParaInTransit_DeveAtualizarStatus()
    {
        // Arrange
        var delivery = new Delivery(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

        // Act
        delivery.ChangeStatus(DeliveryStatus.InTransit);

        // Assert
        Assert.Equal(DeliveryStatus.InTransit, delivery.Status);
    }

    [Theory]
    [InlineData(DeliveryStatus.Delivered)]
    [InlineData(DeliveryStatus.Cancelled)]
    public void ChangeStatus_QuandoEntregaJaFinalizada_DeveLancarException(DeliveryStatus statusFinal)
    {
        // Arrange
        var delivery = new Delivery(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
        delivery.ChangeStatus(statusFinal);

        // Act
        var act = () => delivery.ChangeStatus(DeliveryStatus.InTransit);

        // Assert
        var ex = Assert.Throws<Exception>(act);
        Assert.Equal(
            "It is not possible to change the status of a delivery that has already been completed or canceled.",
            ex.Message);
    }
}
