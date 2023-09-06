namespace Codecool.MarsExploration.Configuration.Service.Logger;

public interface ILogger
{
    public void LogInfo(string message);
    public void LogError(string message);
    public void LogSuccessful(string message);
}