using Codecool.MarsExploration.Calculators.Service;
using Codecool.MarsExploration.MapElements.Model;

namespace Codecool.MarsExploration.MapElements.Service.Builder;

public class MapElementBuilder : IMapElementBuilder
{
    public MapElement Build(int size, string symbol, string name, int dimensionGrowth, string? preferredLocationSymbol = null)
    {
        IDimensionCalculator dimensionCalculator = new DimensionCalculator();
        ICoordinateCalculator coordinateCalculator = new CoordinateCalculator();
        
        var actualBuildSize = dimensionCalculator.CalculateDimension(size, dimensionGrowth);
        var representation = new string[actualBuildSize,actualBuildSize];

        for (var i = 0; i < actualBuildSize; i++)
        {
            for (var j = 0; j < actualBuildSize; j++)
            {
                representation[i, j] = " ";
            }
        }
        
        while (size > 0)
        {
            var randomCoordinate = coordinateCalculator.GetRandomCoordinate(actualBuildSize);

            if (representation[randomCoordinate.X, randomCoordinate.Y] != " ") continue;
            representation[randomCoordinate.X, randomCoordinate.Y] = symbol;
            size--;
        }
        
        var build = new MapElement(representation, name, actualBuildSize, preferredLocationSymbol);
        
        return build;
    }
}
