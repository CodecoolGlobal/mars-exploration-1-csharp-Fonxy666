using System.Drawing;

namespace Codecool.MarsExploration.Configuration.Service.Logger;

public class Logger : ILogger
{
    public void LogInfo(string message)
    {
        LogMessage("INFO", message);
    }

    public void LogError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        LogMessage("ERROR", $"{message}");
        Console.ResetColor();
    }
    
    public void LogSuccessful(string message)
    {
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        LogMessage("SUCCESSFUL", $"{message}");
        Console.ResetColor();
    }

    private void LogMessage(string messageType, string message)
    {
        var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        var formattedMessage = $"{timestamp} [{messageType}] - {message}";
        Console.WriteLine(formattedMessage);
    }
}