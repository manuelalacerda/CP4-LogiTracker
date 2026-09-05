using LogiTracker.Domain.Common;

namespace LogiTracker.Domain.Entities;

public class Carrier : BaseEntity
{
    public string Name { get; private set; }
    public string TaxId { get; private set; }
    
    public List<Vehicle> Vehicles { get; private set; } = new List<Vehicle>(); // permite que a partir de uma transportadora o sistema consiga listar todos os seus veículos.
    public List<Driver> Drivers { get; private set; } = new List<Driver>(); // o mesmo serve aqui só que para motoristas

    public Carrier(string name, string taxId)
    {
        ValidateAndSet(name, taxId);
        Active = true;
    }

    public void UpdateBasicInfo(string name, string taxId)
    {
        ValidateAndSet(name, taxId);
    }

    private void ValidateAndSet(string name, string taxId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new Exception("The carrier's name cannot be empty.");

        if (string.IsNullOrWhiteSpace(taxId) || taxId.Length < 14)
            throw new Exception("The CNPJ (TaxId) must be valid and contain all digits.");

        Name = name;
        TaxId = taxId;
    }
}