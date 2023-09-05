using Codecool.MarsExploration.Configuration.Model;
using Codecool.MarsExploration.MapElements.Model;

namespace Codecool.MarsExploration.MapElements.Service.Generator;

public class MapGenerator : IMapGenerator
{
    public Map Generate(MapConfiguration mapConfig)
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