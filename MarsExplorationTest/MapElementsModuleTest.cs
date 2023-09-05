using Codecool.MarsExploration.Configuration.Model;
using Codecool.MarsExploration.MapElements.Model;
using Codecool.MarsExploration.MapElements.Service.Builder;
using Codecool.MarsExploration.MapElements.Service.Generator;

namespace MarsExplorationTest;

public class MapElementsModuleTest
{
    private readonly string[,] _arr = {{"#", "&"},{"*", "%"}};
    private readonly MapElementBuilder _builder = new MapElementBuilder();
    private readonly MapElementGenerator _generator = new MapElementGenerator();
    private readonly MapElementConfiguration _config;

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

    [Test]
    public void ElementGenTest()
    {
        Dictionary<int, Tuple<string, int, int>> dictionary = new Dictionary<int, Tuple<string, int, int>>();
        Random rnd = new Random();

        dictionary.Add(3, new Tuple<string, int, int>("mountain", rnd.Next(1, 3), rnd.Next(9, 21)));
        dictionary.Add(10, new Tuple<string, int, int>("pit", 0, 10));
        dictionary.Add(0, new Tuple<string, int, int>("mineral", 0, 10));
        dictionary.Add(1, new Tuple<string, int, int>("water", 0, 10));
        
        var config = (new MapConfiguration(1000,0.5, new List<MapElementConfiguration>
        {
            new MapElementConfiguration("#", dictionary.Values.ElementAt(0).Item1, new []
            {
                new ElementToSize(dictionary.Values.ElementAt(0).Item2, dictionary.Values.ElementAt(0).Item3),
                new ElementToSize(rnd.Next(1, 3),  rnd.Next(9, 21))
            }, dictionary.Keys.ElementAt(0)),
            
            new MapElementConfiguration("&", "pit", new[]
            {
                new ElementToSize(2, 10),
                new ElementToSize(1, 20),
            }, 10),
            
            new MapElementConfiguration("%", "mineral", new[]
            {
                new ElementToSize(10, 1)
            }, 0, "#"),
            
            new MapElementConfiguration("*", "water", new[]
            {
                new ElementToSize(10, 1)
            }, dictionary.Keys.ElementAt(2), "&")
            
        }));
        
        var elementList = _generator.CreateAll(config);
        foreach (var each in elementList)
        {
            Console.WriteLine($"{each}\n");
        }
        Assert.That(elementList.Any());
    }
}