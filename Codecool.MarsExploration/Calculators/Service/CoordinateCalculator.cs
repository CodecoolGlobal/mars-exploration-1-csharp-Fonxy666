using Codecool.MarsExploration.Calculators.Model;
using Codecool.MarsExploration.MapElements.Model;

namespace Codecool.MarsExploration.Calculators.Service;

public class CoordinateCalculator : ICoordinateCalculator
{
    private Random _rnd = new Random();
    public Coordinate GetRandomCoordinate(int dimension)
    {
        return new Coordinate(_rnd.Next(0, dimension), _rnd.Next(0, dimension));
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

    public Coordinate GetRandomAdjacentCoordinate(string[,] mapRepresentation, string symbol)
    {
        var coordinates = new List<Coordinate>();
        var dimension = mapRepresentation.GetLength(0);
        for (var i = 0; i < dimension; i++)
        {
            for (var j = 0; j < dimension; j++)
            {
                if (mapRepresentation[i, j] == symbol)
                {
                    coordinates.Add(new Coordinate(i,j));
                }
            }
        }
        var randomAdjacentCoordinates = GetAdjacentCoordinates(coordinates, dimension).ToList();
        
        return randomAdjacentCoordinates[_rnd.Next(0, randomAdjacentCoordinates.Count)];
    }
}