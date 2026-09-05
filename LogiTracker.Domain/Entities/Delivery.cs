using LogiTracker.Domain.Common;
using LogiTracker.Domain.Enums;

namespace LogiTracker.Domain.Entities;

public class Delivery : BaseEntity
{
    public DeliveryStatus Status { get; private set; }
    public DateTime OrderDate { get; private set; }
    public Guid VehicleId { get; private set; }
    public Guid DriverId { get; private set; }
    public Guid CargoId { get; private set; }
    
    public Vehicle Vehicle { get; set; } = null!; //começa o campo vazio, já que o veiculo pode existir sem uma entrega, mas não ao contrario
    public Driver Driver { get; set; } = null!; // o mesmo se aplica para a linha 15 e 16
    public Cargo Cargo { get; set; } = null!; 
    
    public Delivery(Guid vehicleId, Guid driverId, Guid cargoId)
    {
        VehicleId = vehicleId;
        DriverId = driverId;
        CargoId = cargoId;

        OrderDate = DateTime.UtcNow;
        Status = DeliveryStatus.Pending;
        Active = true;
    }

    public void UpdateStatus(DeliveryStatus status)
    {
        Status = status;
    }
    
    public void ChangeStatus(DeliveryStatus newStatus)
    {
        if (Status == DeliveryStatus.Delivered || Status == DeliveryStatus.Cancelled)
            throw new Exception("It is not possible to change the status of a delivery that has already been completed or canceled.");

        Status = newStatus;
    }
}