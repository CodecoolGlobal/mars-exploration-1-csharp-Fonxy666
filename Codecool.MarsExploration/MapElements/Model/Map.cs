namespace Codecool.MarsExploration.MapElements.Model;

public record Map(string?[,] Representation, bool SuccessfullyGenerated = false)
{
    protected static string CreateStringRepresentation(string?[,] arr)
    {
        var representation = "";

        for (var i = 0; i < arr.GetLength(0); i++)
        {
            for (var j = 0; j < arr.GetLength(1); j++)
            {
                representation += arr[i, j];
            }
            representation += "\n";
        }
        
        return representation.Substring(0, representation.Length - 1);
    }

    public override string ToString()
    {
        return CreateStringRepresentation(Representation);
    }
}