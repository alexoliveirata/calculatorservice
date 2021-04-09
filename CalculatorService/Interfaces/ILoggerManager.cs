namespace CalculatorService.Server.Interfaces
{
    public interface ILoggerManager
    {
        void LogInfo(string message);
        void LogWarn(string message);
        void LogDebug(string message);
        void LogError(string message, System.Exception exception = null);
    }
}
