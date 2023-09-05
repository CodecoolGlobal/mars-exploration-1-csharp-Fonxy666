using Codecool.MarsExploration.Calculators.Service;
using Codecool.MarsExploration.MapElements.Model;

namespace Codecool.MarsExploration.MapElements.Service.Builder;

public class MapElementBuilder : IMapElementBuilder
{
    public MapElement Build(int size, string symbol, string name, int dimensionGrowth, string? preferredLocationSymbol = null)
    {
        DimensionCalculator dimensionCalculator = new DimensionCalculator();
        CoordinateCalculator coordinateCalculator = new CoordinateCalculator();
        
        var actualMapSize = dimensionCalculator.CalculateDimension(size, dimensionGrowth);
        string[,] representation = new string[actualMapSize,actualMapSize];

        for (int i = 0; i < actualMapSize; i++)
        {
            for (int j = 0; j < actualMapSize; j++)
            {
                representation[i, j] = " ";
            }
        }
        
        while (size > 0)
        {
            var randomCoordinate = coordinateCalculator.GetRandomCoordinate(actualMapSize);

            if (representation[randomCoordinate.X, randomCoordinate.Y] == " ")
            {
                representation[randomCoordinate.X, randomCoordinate.Y] = symbol;
                size--;
            }
        }
        
        MapElement build = new MapElement(representation, name, actualMapSize, preferredLocationSymbol);
        
        return build;
    }
}
