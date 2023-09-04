using System.Collections;
using Codecool.MarsExploration.Configuration.Model;
using Codecool.MarsExploration.Configuration.Service;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace MarsExplorationTest;

public class Tests
{
    readonly MapElementConfigurationValidator _mapElementConfigurationValidator = new MapElementConfigurationValidator();
    private Dictionary<int, Tuple<string, int, int>> dictionary = new Dictionary<int, Tuple<string, int, int>>();
    private readonly Random _rnd = new Random();

    [Test]
    public void ValidatorTest()
    {
        dictionary.Add(3, new Tuple<string, int, int>("mountain", _rnd.Next(1, 3), _rnd.Next(9, 21)));
        dictionary.Add(10, new Tuple<string, int, int>("pit", 0, 10));
        dictionary.Add(0, new Tuple<string, int, int>("mineral", 0, 10));
        dictionary.Add(1, new Tuple<string, int, int>("water", 0, 10));
        
        var config = _mapElementConfigurationValidator.Validate(new MapConfiguration(1000,0.5, new List<MapElementConfiguration>
        {
            new MapElementConfiguration("#", dictionary.Values.ElementAt(0).Item1, new []
            {
                new ElementToSize(dictionary.Values.ElementAt(0).Item2, dictionary.Values.ElementAt(0).Item3),
                new ElementToSize(_rnd.Next(1, 3),  _rnd.Next(9, 21))
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
        Assert.That(config);
    }
    
    [Test]
    public void ValidatorTestDimension()
    {
        var config = _mapElementConfigurationValidator.Validate(new MapConfiguration(1000,0.5, new List<MapElementConfiguration>
        {
            new MapElementConfiguration("#", "mountain", new []
            {
                new ElementToSize(2, 10),
                new ElementToSize(1, 20),
            }, 4),
            new MapElementConfiguration("&", "pit", new[]
            {
                new ElementToSize(2, 10),
                new ElementToSize(1, 20),
            }, 20),
            new MapElementConfiguration("%", "mineral", new[]
            {
                new ElementToSize(10, 1)
            }, 1, "#"),
            new MapElementConfiguration("*", "water", new[]
            {
                new ElementToSize(10, 1)
            }, 3, "&")
        }));
        Assert.That(!config);
    }
    
    [Test]
    public void ValidatorTestSize()
    {
        var config = _mapElementConfigurationValidator.Validate(new MapConfiguration(1000,0.5, new List<MapElementConfiguration>
        {
            new MapElementConfiguration("#", "mountain", new []
            {
                new ElementToSize(2, 200),
                new ElementToSize(1, 100),
            }, 3),
            new MapElementConfiguration("&", "pit", new[]
            {
                new ElementToSize(2, 200),
                new ElementToSize(1, 200),
            }, 10),
            new MapElementConfiguration("%", "mineral", new[]
            {
                new ElementToSize(10, 200)
            }, 0, "#"),
            new MapElementConfiguration("*", "water", new[]
            {
                new ElementToSize(10, 200)
            }, 0, "&")
        }));
        Assert.That(!config);
    }
    
    [Test]
    public void ValidatorTestName()
    {
        var config = _mapElementConfigurationValidator.Validate(new MapConfiguration(1000,0.5, new List<MapElementConfiguration>
        {
            new MapElementConfiguration("#", "mountains", new []
            {
                new ElementToSize(2, 10),
                new ElementToSize(1, 20),
            }, 3),
            new MapElementConfiguration("&", "pits", new[]
            {
                new ElementToSize(2, 10),
                new ElementToSize(1, 20),
            }, 10),
            new MapElementConfiguration("%", "minerals", new[]
            {
                new ElementToSize(10, 1)
            }, 0, "#"),
            new MapElementConfiguration("*", "waters", new[]
            {
                new ElementToSize(10, 1)
            }, 0, "&")
        }));
        Assert.That(!config);
    }
}