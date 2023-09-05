using System.Text;

namespace Codecool.MarsExploration.MapElements.Model;

public record Map(string?[,] Representation, bool SuccessfullyGenerated = false)
{
    protected static string CreateStringRepresentation(string?[,] arr)
    {
        string representation = "";

        for (int i = 0; i < arr.GetLength(0); i++)
        {
            for (int j = 0; j < arr.GetLength(1); j++)
            {
                representation += arr[i, j];
            }

            representation += "\n";
        }
        
        return representation.Trim();
    }

    public override string ToString()
    {
        return CreateStringRepresentation(Representation);
    }
}
