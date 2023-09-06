using Codecool.MarsExploration.Configuration.Model;
using Codecool.MarsExploration.Configuration.Service.Logger;
using Codecool.MarsExploration.MapElements.Service.Generator;

internal class Program
{
    private static readonly string WorkDir = AppDomain.CurrentDomain.BaseDirectory;
    private static readonly ILogger Logger = new Logger();

    public static void Main(string[] args)
    {
        Logger.LogInfo("Mars Exploration Sprint 1");

        IMapGenerator mapGenerator = new MapGenerator();
        var mapConfig = GetConfiguration();
        CreateAndWriteMaps(3, mapGenerator, mapConfig);
        
        Logger.LogSuccessful("Mars maps successfully generated.");
    }

    private static void CreateAndWriteMaps(int count, IMapGenerator mapGenerator, MapConfiguration mapConfig)
    {
        var maps = new List<string>();
        var i = 1;
        while (maps.Count != count)
        {
            Logger.LogInfo($"#{i}. tried to generate: ");
            var finishedMap = mapGenerator.Generate(mapConfig);
            if (finishedMap.SuccessfullyGenerated)
            {
                Logger.LogSuccessful("Successfully generated!");
                maps.Add(finishedMap.ToString());
            }
            i++;
        }

        for (var j = 0; j < maps.Count; j++)
        {
            var fileWriter = new[] { $"Map-{j+1} \n {maps[j]}" };
            var filePath = Path.Combine(WorkDir, "Resources", $"Map-{j+1}.txt");
            Directory.CreateDirectory(Path.GetDirectoryName(filePath) ?? string.Empty);
            File.WriteAllLines(filePath, fileWriter);
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
