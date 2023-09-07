using Codecool.MarsExploration.Configuration.Model;
using Codecool.MarsExploration.MapElements.Model;
using Codecool.MarsExploration.MapElements.Service.Builder;

namespace Codecool.MarsExploration.MapElements.Service.Generator;

public class MapElementGenerator : IMapElementsGenerator
{
    public IEnumerable<MapElement> CreateAll(MapConfiguration mapConfig)
    {
        var mapElementBuilder = new MapElementBuilder();
        var mapElements = new List<MapElement>();
        
        foreach (var mapConfigElement in mapConfig.MapElementConfigurations)
        {
            foreach (var elementToSize in mapConfigElement.ElementsToDimensions)
            {
                for (var i = 0; i < elementToSize.ElementCount; i++)
                {
                    mapElements.Add(mapElementBuilder.Build(
                        elementToSize.Size,
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
