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
        var elementList = mapElementsGenerator.CreateAll(mapConfig).ToList();
        int dimension = (int)Math.Floor(Math.Sqrt(mapConfig.MapSize));

        string[,] map = new string[dimension, dimension];
        map = MapCleaner(map);

        var buildCounter = 0;
        for (int i = 0; i < elementList.Count(); i++)
        {
            if (buildCounter > 10)
            {
                Console.WriteLine("Map wasn't generated!");
                return new Map(map, false);
            }
            switch (elementList[i].PreferredLocationSymbol)
            {
                case null:
                    for (int j = 0; j < 1000; j++)
                    {
                        if (j == 999)
                        {
                            i = -1;
                            buildCounter++;
                            map = MapCleaner(map);
                            break;
                        }

                        var randomCoordinate = coordinateCalculator.GetRandomCoordinate(map.GetLength(0));
                        if (!mapElementPlacer.CanPlaceElement(elementList[i], map, randomCoordinate)) continue;
                        //Console.WriteLine($"{mapElement.Name}: {randomCoordinate.X}, {randomCoordinate.Y} || dimension:{mapElement.Dimension}");
                        mapElementPlacer.PlaceElement(elementList[i], map, randomCoordinate);
                        break;
                       
                    }
                    
                    break;
                case "#":
                case "&":
                    for (int j = 0; j < 1000; j++)
                    {
                        if (j == 999)
                        {
                            i = -1;
                            buildCounter++;
                            map = MapCleaner(map);
                            break;
                        }
                        var randomAdjacentCoordinate = coordinateCalculator.GetRandomAdjacentCoordinate(map, elementList[i].PreferredLocationSymbol);
                        if (!mapElementPlacer.CanPlaceElement(elementList[i], map, randomAdjacentCoordinate)) continue;
                        //Console.WriteLine($"{mapElement.Name}: {randomAdjacentCoordinate.X}, {randomAdjacentCoordinate.Y} || dimension:{mapElement.Dimension}");
                        mapElementPlacer.PlaceElement(elementList[i], map, randomAdjacentCoordinate);
                        break;
                    }

                    break;
            }
        }
        
        return new Map(map, true);
    }

    private string[,] MapCleaner(string[,] map)
    {
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                map[i, j] = " ";
            }
        }

        return map;
    }
    
}

/*
public record MapConfiguration(
    int MapSize,
    double ElementToSpaceRatio,
    IEnumerable<MapElementConfiguration> MapElementConfigurations);
*/