using Codecool.MarsExploration.Configuration.Model;
using Codecool.MarsExploration.MapElements.Model;
using Codecool.MarsExploration.MapElements.Service.Builder;

namespace Codecool.MarsExploration.MapElements.Service.Generator;

public class MapElementGenerator : IMapElementsGenerator
{
    public IEnumerable<MapElement> CreateAll(MapConfiguration mapConfig)
    {
        MapElementBuilder mapElementBuilder = new MapElementBuilder();;
        List<MapElement> mapElements = new List<MapElement>();
        
        foreach (var mapConfigElement in mapConfig.MapElementConfigurations)
        {
            foreach (var elementToSize in mapConfigElement.ElementsToDimensions)
            {
                for (int i = 0; i < elementToSize.ElementCount; i++)
                {
                    mapElements.Add(mapElementBuilder.Build(elementToSize.Size,
                                                            mapConfigElement.Symbol,
                                                            mapConfigElement.Name,
                                                            mapConfigElement.DimensionGrowth,
                                                            mapConfigElement.PreferredLocationSymbol));
                }
            }
        }
        
        return mapElements;
    }
}

/*
public record MapConfiguration(
    int MapSize,
    double ElementToSpaceRatio,
    IEnumerable<MapElementConfiguration> MapElementConfigurations);
    
public MapElement Build(int size, string symbol, string name, int dimensionGrowth, string? preferredLocationSymbol = null)

public record MapElement(string?[,] Representation, string Name, int Dimension, string? PreferredLocationSymbol = null) : Map(Representation)
*/