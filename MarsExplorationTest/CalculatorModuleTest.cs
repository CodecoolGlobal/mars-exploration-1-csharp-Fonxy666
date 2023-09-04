using Codecool.MarsExploration.Calculators.Model;
using Codecool.MarsExploration.Calculators.Service;
using NUnit.Framework.Internal;

namespace MarsExplorationTest;

public class CalculatorModuleTest
{
    private readonly DimensionCalculator _dimensionCalculator = new DimensionCalculator();
    private readonly CoordinateCalculator _coordinateCalculator = new CoordinateCalculator();

    [Test]
    public void DimensionCalculatorTest()
    {
        var test = _dimensionCalculator.CalculateDimension(10, 2);
        
        Assert.That(test, Is.EqualTo(6));
    }

    [Test]
    public void AdjacentCoordinatesTest()
    {
        var dimension = 4;
        var randomCoordinate = _coordinateCalculator.GetRandomCoordinate(dimension);
        
        var test = _coordinateCalculator.GetAdjacentCoordinates(randomCoordinate, dimension).ToList();

        foreach (var coordinate in test)
        {
            Assert.That(coordinate.X >= 0 && coordinate.X < dimension && coordinate.Y >= 0 && coordinate.Y < dimension);
        }
        
        Assert.That(test, Does.Not.Contain(randomCoordinate));
    }
}
