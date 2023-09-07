using Codecool.MarsExploration.Configuration.Model;

namespace Codecool.MarsExploration.Configuration.Service;

public class MapElementConfigurationValidator : IMapConfigurationValidator
{
    public bool Validate(MapConfiguration mapConfig)
    {
        return ValidateAllowedElements(mapConfig) && ValidateElementsDimension(mapConfig);
    }

    private bool ValidateAllowedElements(MapConfiguration mapConfig)
    {
        var maxAllowedElementCount = mapConfig.MapSize * mapConfig.ElementToSpaceRatio;
        var elementsSizeCount = mapConfig.MapElementConfigurations
            .Sum(mapConfigMapElementConfiguration => mapConfigMapElementConfiguration.ElementsToDimensions
                .Sum(elementsToDimension => elementsToDimension.ElementCount * elementsToDimension.Size));

        return maxAllowedElementCount >= elementsSizeCount;
    }

    private bool ValidateElementsDimension(MapConfiguration mapConfig)
    {
        foreach (var mapConfigMapElementConfiguration in mapConfig.MapElementConfigurations)
        {
            switch (mapConfigMapElementConfiguration.Name)
            {
                case "mountain":
                    if (mapConfigMapElementConfiguration.DimensionGrowth != 3)
                        return false;
                    break;
                case "pit":
                    if (mapConfigMapElementConfiguration.DimensionGrowth != 10)
                        return false;
                    break;
                case "mineral":
                case "water":
                    if (mapConfigMapElementConfiguration.DimensionGrowth != 0)
                        return false;
                    break;
                default :
                    return false;
            }
        }

        return true;
    }
}