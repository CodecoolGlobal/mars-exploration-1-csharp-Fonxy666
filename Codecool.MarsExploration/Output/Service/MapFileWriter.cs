using Codecool.MarsExploration.Configuration.Service.Logger;
using Codecool.MarsExploration.MapElements.Model;

namespace Codecool.MarsExploration.Output.Service;

public class MapFileWriter : IMapFileWriter
{
    public void WriteMapFile(string map, string file, int count)
    {
        var logger = new Logger();
        try
        {
            var fileWriter = new[] { $"Map-{count+1} \n {map}" };
            Directory.CreateDirectory(Path.GetDirectoryName(file) ?? string.Empty);
            File.WriteAllLines(file, fileWriter);
            logger.LogSuccessful($"Map-{count+1} successfully written to file!");
        }
        catch (IOException e)
        {
            logger.LogError(e.ToString());
            throw;
        }
    }
}