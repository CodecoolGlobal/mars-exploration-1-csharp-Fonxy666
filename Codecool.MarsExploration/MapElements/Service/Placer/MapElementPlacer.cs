using Codecool.MarsExploration.Calculators.Model;
using Codecool.MarsExploration.MapElements.Model;

namespace Codecool.MarsExploration.MapElements.Service.Placer;

public class MapElementPlacer : IMapElementPlacer
{
    public bool CanPlaceElement(MapElement element, string?[,] map, Coordinate coordinate)
    {
        var vertical = coordinate.X + element.Dimension;
        var horizontal = coordinate.Y + element.Dimension;

        if (vertical > map.GetLength(0) || horizontal > map.GetLength(1)) return false;

        for (var i = coordinate.X; i < vertical; i++)
        {
            for (var j = coordinate.Y; j < horizontal; j++)
            {
                if (map[i, j] != " ") return false;
            }
        }
        
        return true;
    }

    public void PlaceElement(MapElement element, string?[,] map, Coordinate coordinate)
    {
        var vertical = coordinate.X + element.Dimension;
        var horizontal = coordinate.Y + element.Dimension;
        
        for (var i = coordinate.X; i < vertical; i++)
        {
            for (var j = coordinate.Y; j < horizontal; j++)
            {
                map[i, j] = element.Representation[i - coordinate.X, j - coordinate.Y];
            }
        }
    }
}
