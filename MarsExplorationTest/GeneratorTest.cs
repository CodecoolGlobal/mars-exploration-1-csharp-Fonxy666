using Codecool.MarsExploration.Calculators.Model;
using Codecool.MarsExploration.Calculators.Service;
using Codecool.MarsExploration.Configuration.Model;
using Codecool.MarsExploration.MapElements.Service.Generator;

namespace MarsExplorationTest;

public class GeneratorTest
{
    [Test]
    public void MapGeneratorTest()
    {
         IMapGenerator _mapGenerator = new MapGenerator();
         ICoordinateCalculator coordinateCalculator = new CoordinateCalculator();
         var finishedMap = _mapGenerator.Generate(GetConfiguration()).Representation;
         for (int i = 0; i < finishedMap.GetLength(0); i++)
         {
             for (int j = 0; j < finishedMap.GetLength(1); j++)
             {
                 if (finishedMap[i,j] == "%")
                 {
                     var foundCorrectAdjacentElement = false;
                     var adjacentCoordinates =
                         coordinateCalculator.GetAdjacentCoordinates(new Coordinate(i, j), finishedMap.GetLength(0));
                     foreach (var adjacentCoordinate in adjacentCoordinates)
                     {
                         Console.WriteLine("asdf");
                         if (finishedMap[adjacentCoordinate.X, adjacentCoordinate.Y] == "#")
                         {
                             foundCorrectAdjacentElement = true;
                         }
                     }
                     
                     Assert.That(foundCorrectAdjacentElement);
                     
                 }
             }
         }
         

    }
    private static MapConfiguration GetConfiguration()
    {
        const string mountainSymbol = "#";
        const string pitSymbol = "&"; 
        const string mineralSymbol = "%";
        const string waterSymbol = "*";

        var mountainsCfg = new MapElementConfiguration(mountainSymbol, "mountain", new[]
        {
            new ElementToSize(2, 20),
            new ElementToSize(1, 30),
        }, 3);
        
        var pitsCfg = new MapElementConfiguration(pitSymbol, "pit", new[]
        {
            new ElementToSize(2, 10),
            new ElementToSize(1, 20),
        }, 10);
        
        var mineralsCfg = new MapElementConfiguration(mineralSymbol, "mineral", new[]
        {
            new ElementToSize(10, 1)
        }, 0, "#");
        
        var watersCfg = new MapElementConfiguration(waterSymbol, "water", new[]
        {
            new ElementToSize(10, 1)
        }, 0, "&");

        List<MapElementConfiguration> elementsCfg = new() { mountainsCfg, pitsCfg, mineralsCfg, watersCfg };
        return new MapConfiguration(2000, 0.5, elementsCfg);
    }

    
}