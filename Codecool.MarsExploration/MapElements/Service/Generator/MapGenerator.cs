using Codecool.MarsExploration.Calculators.Model;
using Codecool.MarsExploration.Calculators.Service;
using Codecool.MarsExploration.Configuration.Model;
using Codecool.MarsExploration.Configuration.Service.Logger;
using Codecool.MarsExploration.MapElements.Model;
using Codecool.MarsExploration.MapElements.Service.Placer;

namespace Codecool.MarsExploration.MapElements.Service.Generator;

public class MapGenerator : IMapGenerator
{
    private readonly IMapElementPlacer _mapElementPlacer = new MapElementPlacer();
    
    public Map Generate(MapConfiguration mapConfig)
    {
        IMapElementsGenerator mapElementsGenerator = new MapElementGenerator();
        ICoordinateCalculator coordinateCalculator = new CoordinateCalculator();
        ILogger logger = new Logger();
        
        var elementList = mapElementsGenerator.CreateAll(mapConfig).ToList();
        var dimension = (int)Math.Floor(Math.Sqrt(mapConfig.MapSize));
        var map = new string[dimension, dimension];
        var buildCounter = 0;
        
        map = MapCleaner(map);
        
        for (var i = 0; i < elementList.Count; i++)
        {
            if (buildCounter > 10)
            {
                logger.LogError("Map wasn't generated!");
                return new Map(map, false);
            }
            
            for (var j = 0; j < 1000; j++)
            {
                if (j == 999)
                {
                    i = -1;
                    buildCounter++;
                    map = MapCleaner(map);
                    break;
                }
                
                var randomCoordinate = elementList[i].PreferredLocationSymbol == null ? 
                    coordinateCalculator.GetRandomCoordinate(map.GetLength(0)) :
                    coordinateCalculator.GetRandomAdjacentCoordinate(map, elementList[i].PreferredLocationSymbol!);
                        
                if (!_mapElementPlacer.CanPlaceElement(elementList[i], map, randomCoordinate)) continue;
                _mapElementPlacer.PlaceElement(elementList[i], map, randomCoordinate);
                break;
            }
        }
        
        return new Map(map, true);
    }
    
    private string[,] MapCleaner(string[,] map)
    {
        for (var i = 0; i < map.GetLength(0); i++)
        {
            for (var j = 0; j < map.GetLength(1); j++)
            {
                map[i, j] = " ";
            }
        }

        return map;
    }
}
