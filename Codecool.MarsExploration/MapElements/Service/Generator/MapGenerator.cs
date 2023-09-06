using Codecool.MarsExploration.Calculators.Service;
using Codecool.MarsExploration.Configuration.Model;
using Codecool.MarsExploration.MapElements.Model;
using Codecool.MarsExploration.MapElements.Service.Placer;

namespace Codecool.MarsExploration.MapElements.Service.Generator;

public class MapGenerator : IMapGenerator
{
    public Map Generate(MapConfiguration mapConfig)
    {
        IMapElementsGenerator mapElementsGenerator = new MapElementGenerator();
        ICoordinateCalculator coordinateCalculator = new CoordinateCalculator();
        IMapElementPlacer mapElementPlacer = new MapElementPlacer();
        var elementList = mapElementsGenerator.CreateAll(mapConfig);
        int dimension = (int)Math.Floor(Math.Sqrt(mapConfig.MapSize));

        string[,] map = new string[dimension, dimension];
        for (int i = 0; i < dimension; i++)
        {
            for (int j = 0; j < dimension; j++)
            {
                map[i, j] = " ";
            }
        }
        
        foreach (var mapElement in elementList)
        {
            switch (mapElement.PreferredLocationSymbol)
            {
                case null:
                    var elementPlaced = false;
                    while (!elementPlaced)
                    {
                        var randomCoordinate = coordinateCalculator.GetRandomCoordinate(map.GetLength(0));
                        if (!mapElementPlacer.CanPlaceElement(mapElement, map, randomCoordinate)) continue;
                        //Console.WriteLine($"{mapElement.Name}: {randomCoordinate.X}, {randomCoordinate.Y} || dimension:{mapElement.Dimension}");
                        mapElementPlacer.PlaceElement(mapElement, map, randomCoordinate);
                        elementPlaced = true;
                    }

                    break;
                case "#":
                case "&":
                    var adjacentElementPlaced = false;
                    
                    while (!adjacentElementPlaced)
                    {
                        
                        var randomAdjacentCoordinate = coordinateCalculator.GetRandomAdjacentCoordinate(map, mapElement.PreferredLocationSymbol);
                        if (!mapElementPlacer.CanPlaceElement(mapElement, map, randomAdjacentCoordinate)) continue;
                        //Console.WriteLine($"{mapElement.Name}: {randomAdjacentCoordinate.X}, {randomAdjacentCoordinate.Y} || dimension:{mapElement.Dimension}");
                        mapElementPlacer.PlaceElement(mapElement, map, randomAdjacentCoordinate);
                        adjacentElementPlaced = true;
                    }

                    break;
                    
            }
        }

        return new Map(map, true);
    }
    
    
}

/*
public record MapConfiguration(
    int MapSize,
    double ElementToSpaceRatio,
    IEnumerable<MapElementConfiguration> MapElementConfigurations);
*/