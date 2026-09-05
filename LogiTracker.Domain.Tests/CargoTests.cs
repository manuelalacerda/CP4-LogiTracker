using LogiTracker.Domain.Entities;

namespace LogiTracker.Domain.Tests;

public class CargoTests
{
    [Fact]
    public void Construtor_ComDadosValidos_DeveCriarCargaAtiva()
    {
        // Arrange
        const string description = "Eletrônicos diversos";
        const int weight = 1200;
        const int monetaryValue = 350_000;

        // Act
        var cargo = new Cargo(description, weight, monetaryValue);

        // Assert
        Assert.Equal(description, cargo.Description);
        Assert.Equal(weight, cargo.Weight);
        Assert.Equal(monetaryValue, cargo.MonetaryValue);
        Assert.True(cargo.Active);
    }

    [Theory]
    [InlineData(0, 100)]
    [InlineData(-10, 100)]
    [InlineData(50, -1)]
    public void Construtor_ComPesoOuValorInvalido_DeveLancarException(int weight, int monetaryValue)
    {
        // Arrange
        const string description = "Carga de teste";

        // Act
        var act = () => new Cargo(description, weight, monetaryValue);

        // Assert
        Assert.Throws<Exception>(act);
    }

    [Fact]
    public void UpdateValue_ComValorNegativo_DeveLancarException()
    {
        // Arrange
        var cargo = new Cargo("Carga de teste", 100, 500);

        // Act
        var act = () => cargo.UpdateValue(-1);

        // Assert
        var ex = Assert.Throws<Exception>(act);
        Assert.Equal("The monetary value cannot be negative.", ex.Message);
    }
}
