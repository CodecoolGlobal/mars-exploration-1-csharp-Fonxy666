using Codecool.MarsExploration.Configuration.Model;
using Codecool.MarsExploration.MapElements.Model;

namespace Codecool.MarsExploration.MapElements.Service.Generator;

public class MapElementGenerator : IMapElementsGenerator
{
    public IEnumerable<MapElement> CreateAll(MapConfiguration mapConfig)
    {
        throw new NotImplementedException();
    }
}

/*
public record MapConfiguration(
    int MapSize,
    double ElementToSpaceRatio,
    IEnumerable<MapElementConfiguration> MapElementConfigurations);
*/