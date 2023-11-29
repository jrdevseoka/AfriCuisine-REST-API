using Africuisine.Application.Interfaces.Log;
using NLog;

namespace Africuisine.Infrastructure.Services.Log
{
    public class NLogger : INLogger
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        public void Error(string message, Exception exception)
        {
            Logger.Error(exception, message);
        }

        public void Info(string message)
        {
            Logger.Info(message);
        }

        public void Warn(string message)
        {
            Logger.Warn(message);   
        }
    }
}
