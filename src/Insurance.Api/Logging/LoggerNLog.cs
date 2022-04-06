using NLog;
using System;
namespace Insurance.Api.Logging
{
    public class LoggerNLog : ILog
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        public void Debug(Exception e, string message)
        {
            logger.Debug(e,message);
        }

        public void Error(Exception e, string message)
        {
            logger.Error(e,message);
        }

        public void Information(Exception e, string message)
        {
            logger.Info(e, message);
        }

        public void Warning(Exception e, string message)
        {
            logger.Warn(e, message);
        }
    }
}
