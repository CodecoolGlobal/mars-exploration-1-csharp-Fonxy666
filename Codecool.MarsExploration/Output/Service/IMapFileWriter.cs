using Codecool.MarsExploration.MapElements.Model;

namespace Codecool.MarsExploration.Output.Service;

public interface IMapFileWriter
{
    public void WriteMapFile(string map, string file, int count);
}
