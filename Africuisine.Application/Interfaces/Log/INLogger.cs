namespace Africuisine.Application.Interfaces.Log
{
    public interface INLogger
    {
        void Info(string message);
        void Warn(string message);
        void Error(Exception exception, string message);
    }
}
