using LogiTracker.Domain.Common;

namespace LogiTracker.Domain.Entities;

public class Vehicle : BaseEntity
{
    public string Plate { get; private set; }

    public string Model { get; private set; }

    public Guid CarrierId { get; private set; }
    
    public Carrier Carrier { get; set; } = null!; // na minha logica todo carro já nasce com transportadora (não importa se ele foi comprado e cadastrado antes de ter transportadora), mas isso pode ser definido depois por meio de codigo, por isso crio ele nulo
    public List<Delivery> Deliveries { get; private set; } = new List<Delivery>();

    public Vehicle(string plate, string model, Guid carrierId)
    {
        Plate = plate;
        Model = model;
        CarrierId = carrierId;
        Active = true;
    }

    public void UpdateModel(string model)
    {
        Model = model;
    }
    
    public void UpdatePlate(string newPlate)
    {
        if (string.IsNullOrWhiteSpace(newPlate) || newPlate.Length < 7)
            throw new Exception("The license plate must be provided and have at least 7 characters.");

        Plate = newPlate.ToUpper(); // não lembro se o senhor já mostrou isso em sala, mas só usei para padroniza as letras em maiúsculo
    }
}