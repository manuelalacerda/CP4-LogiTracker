using LogiTracker.Domain.Common;

namespace LogiTracker.Domain.Entities;

public class Cargo : BaseEntity
{
    public string Description { get; private set; }
    public int Weight { get; private set; } // pensei em usar em gramas não sei se é muito absurdo, não lembro de você ter mostrado em sala algo como decimal ou float então decidi não utilizar
    public int MonetaryValue { get; private set; } // aqui seria em centavos ou reais inteiros, já que utilizei int.
    
    public Delivery? Delivery { get; set; }
    
    public Cargo(string description, int weight, int monetaryValue)
    {
        Description = description;
        Weight = weight;
        MonetaryValue = monetaryValue;
        Validate();
        Active = true;
    }

    public void UpdateValue(int monetaryValue)
    {
        MonetaryValue = monetaryValue;
        Validate();
    }
    
    private void Validate()
    {
        if (Weight <= 0) 
            throw new Exception("The weight must be more then zero.");
        
        if (MonetaryValue < 0)
            throw new Exception("The monetary value cannot be negative.");
    }
}