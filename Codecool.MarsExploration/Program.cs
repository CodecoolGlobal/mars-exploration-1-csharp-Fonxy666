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
        var mapConfig = GetConfiguration();

        IDimensionCalculator dimensionCalculator = new DimensionCalculator();
        ICoordinateCalculator coordinateCalculator = new CoordinateCalculator();

        IMapElementBuilder mapElementFactory = new MapElementBuilder();
        IMapElementsGenerator mapElementsGenerator = new MapElementGenerator();

        IMapConfigurationValidator mapConfigValidator = new MapElementConfigurationValidator();
        IMapElementPlacer mapElementPlacer = new MapElementPlacer();

        IMapGenerator mapGenerator = null;

        var list = mapElementsGenerator.CreateAll(mapConfig);
        string[,] map = new string[10, 10];
        
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(0); j++)
            {
                map[i, j] = " ";
            }
        }
        
        foreach (var element in list)
        {
            while (true)
            {
                var newCoord = coordinateCalculator.GetRandomCoordinate(map.GetLength(0));
                if (mapElementPlacer.CanPlaceElement(element, map, newCoord))
                {
                    mapElementPlacer.PlaceElement(element, map, newCoord);
                    break;
                }
            }
        }
        
        Map mapped = new Map(map);

        Console.WriteLine("Map:");
        Console.WriteLine(mapped.ToString());
        
        CreateAndWriteMaps(3, mapGenerator, mapConfig);
        
        Console.WriteLine("Mars maps successfully generated.");
        /*Console.ReadKey();*/
    }

    private static void CreateAndWriteMaps(int count, IMapGenerator mapGenerator, MapConfiguration mapConfig)
    {
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
        return new MapConfiguration(1000, 0.5, elementsCfg);
    }
}
