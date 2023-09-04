using Codecool.MarsExploration.Calculators.Model;

namespace Codecool.MarsExploration.Calculators.Service;

public class CoordinateCalculator : ICoordinateCalculator
{
    public Coordinate GetRandomCoordinate(int dimension)
    {
        var rnd = new Random();
        return new Coordinate(rnd.Next(0, dimension), rnd.Next(0, dimension));
    }

    public IEnumerable<Coordinate> GetAdjacentCoordinates(Coordinate coordinate, int dimension)
    {
        var adjacentCoordinates = new List<Coordinate>();

        var x = coordinate.X;
        var y = coordinate.Y;
        
        var offsets = new List<(int, int)>
        {
            (-1, 0), (1, 0), (0, -1), (0, 1), (-1, -1), (-1, 1), (1, -1), (1, 1)
        };

        foreach (var (dx, dy) in offsets)
        {
            var newX = x + dx;
            var newY = y + dy;

            if (newX >= 0 && newX < dimension && newY >= 0 && newY < dimension)
            {
                adjacentCoordinates.Add(new Coordinate(newX, newY));
            }
        }

        return adjacentCoordinates;
    }

    public IEnumerable<Coordinate> GetAdjacentCoordinates(IEnumerable<Coordinate> coordinates, int dimension)
    {
        var newList = new List<Coordinate>();
        foreach (var coordinate in coordinates)
        {
            var givenCoordinateList = GetAdjacentCoordinates(coordinate, dimension);
            foreach (var givenCoordinate in givenCoordinateList)
            {
                if (!newList.Contains(givenCoordinate))
                {
                    newList.Add(givenCoordinate);
                }
            }
        }

        return newList;
    }
}