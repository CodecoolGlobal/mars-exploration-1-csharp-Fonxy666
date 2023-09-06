using Codecool.MarsExploration.Calculators.Service;
using Codecool.MarsExploration.Configuration.Model;
using Codecool.MarsExploration.Configuration.Service;
using Codecool.MarsExploration.MapElements.Model;
using Codecool.MarsExploration.MapElements.Service.Builder;
using Codecool.MarsExploration.MapElements.Service.Generator;
using Codecool.MarsExploration.MapElements.Service.Placer;

internal class Program
{
    //You can change this to any directory you like
    private static readonly string WorkDir = AppDomain.CurrentDomain.BaseDirectory;

    public static void Main(string[] args)
    {
        Console.WriteLine("Mars Exploration Sprint 1");
        

       
    /*
        var list = mapElementsGenerator.CreateAll(mapConfig);
        string[,] map = new string[100, 100];
        
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(0); j++)
            {
                map[i, j] = " ";
            }
        }
        
        foreach (var element in list)
        {
            bool elementPlaced = false;
            while (!elementPlaced)
            {
                //Console.WriteLine(element.PreferredLocationSymbol);
                
                if (element.PreferredLocationSymbol != null)
                {
                    var newCoord = coordinateCalculator.GetRandomCoordinate(map.GetLength(0));
                    if (map[newCoord.X, newCoord.Y] == element.PreferredLocationSymbol)
                    {
                        var adjacentCoordinates = coordinateCalculator.GetAdjacentCoordinates(newCoord, element.Dimension);
                        foreach (var adjacentCoordinate in adjacentCoordinates)
                        {
                            if (mapElementPlacer.CanPlaceElement(element, map, adjacentCoordinate))
                            {
                                //Console.WriteLine($"{element.Name}: {newCoord.X}, {newCoord.Y} || dimension:{element.Dimension}");
                                mapElementPlacer.PlaceElement(element, map, adjacentCoordinate);
                                elementPlaced = true;
                            }
                        }
                    }
                }
                else
                {
                    var newCoord = coordinateCalculator.GetRandomCoordinate(map.GetLength(0));
                    if (mapElementPlacer.CanPlaceElement(element, map, newCoord))
                    {
                        //Console.WriteLine($"{element.Name}: {newCoord.X}, {newCoord.Y} || dimension:{element.Dimension}");
                        mapElementPlacer.PlaceElement(element, map, newCoord);
                        elementPlaced = true;
                    }
                }
                
            }
        }
        
        Map mapped = new Map(map);
        */
        Console.WriteLine("Maps:");
        IMapGenerator mapGenerator = new MapGenerator();
        var mapConfig = GetConfiguration();
        CreateAndWriteMaps(10, mapGenerator, mapConfig);
        
        Console.WriteLine("Mars maps successfully generated.");
        /*Console.ReadKey();*/
    }

    private static void CreateAndWriteMaps(int count, IMapGenerator mapGenerator, MapConfiguration mapConfig)
    {
        List<string> maps = new List<string>();
        var i = 1;
        while (maps.Count() != count)
        {
            Console.WriteLine($"Map #{i}");
            var finishedMap = mapGenerator.Generate(mapConfig);
            if (finishedMap.SuccessfullyGenerated)
            {
                maps.Add(finishedMap.ToString());
                Console.WriteLine("Successfully generated!");
            }
            i++;
        }
        
        foreach (var map in maps)
        {
            Console.WriteLine(map);
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
        return new MapConfiguration(1200, 0.5, elementsCfg);
    }
}
