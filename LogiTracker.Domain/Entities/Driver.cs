using LogiTracker.Domain.Common;

namespace LogiTracker.Domain.Entities;

public class Driver : BaseEntity
{
    public string Name { get; private set; }
    public string LicenseNumber { get; private set; }
    public Guid CarrierId { get; private set; }
    
    public Carrier Carrier { get; set; } = null!; //usei o null! porque o motorista sempre pertence a uma transportadora mas o valor começa nulo apenas para facilitar a criação do objeto e o operador ! garante que esse campo seja obrigatório no restante do programa.
    public List<Delivery> Deliveries { get; private set; } = new List<Delivery>();

    public Driver(string name, string licenseNumber, Guid carrierId)
    {
        Name = name;
        LicenseNumber = licenseNumber;
        CarrierId = carrierId;
        Active = true;
    }

    public void UpdateLicense(string licenseNumber)
    {
        LicenseNumber = licenseNumber;
    }
}