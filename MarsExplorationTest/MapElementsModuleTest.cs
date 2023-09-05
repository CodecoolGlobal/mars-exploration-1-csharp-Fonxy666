using Codecool.MarsExploration.MapElements.Model;
using Codecool.MarsExploration.MapElements.Service.Builder;

namespace MarsExplorationTest;

public class MapElementsModuleTest
{
    private readonly string[,] _arr = {{"#", "&"},{"*", "%"}};
    private readonly MapElementBuilder _builder = new MapElementBuilder();

    [Test]
    public void MapTest()
    {
        Map island = new(_arr, true);
        Assert.That(island.ToString(), Is.EqualTo("#&\n*%"));
    }

    [Test]
    public void BuildTest()
    {
        var firstMountain = _builder.Build(20, "#", "Mountain", 3);
        
        Assert.Multiple(() =>
        {
            Assert.That(firstMountain.Name, Is.EqualTo("Mountain"));
            Assert.That(firstMountain.PreferredLocationSymbol, Is.EqualTo(null));
        });
    }
}