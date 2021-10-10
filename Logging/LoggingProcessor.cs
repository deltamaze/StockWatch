using System;

namespace StockWatch.Logging
{
    public class LoggingProcessor : ILoggingProcessor
    {
        private ILogger[] loggers;
        public LoggingProcessor(ILogger[] loggers)
        {
            this.loggers = loggers;
        }
        private string LogTime()
        {
            return DateTime.Now.ToString("yyyyMMddhhmmss");
        }
        public void Trace(string Message)
        {
            foreach (ILogger logger in loggers)
            {
                logger.Trace($"{LogTime()} Trace: {Message}");
            }
        }
        public void Info(string Message)
        {
            foreach (ILogger logger in loggers)
            {
                logger.Info($"{LogTime()} Info: {Message}");
            }
        }
        public void Debug(string Message)
        {
            foreach (ILogger logger in loggers)
            {
                logger.Debug($"{LogTime()} Debug: {Message}");
            }
        }
        public void Error(string Message)
        {
            foreach (ILogger logger in loggers)
            {
                logger.Error($"{LogTime()} Error: {Message}");
            }
        }
        public void Warn(string Message)
        {
            foreach (ILogger logger in loggers)
            {
                logger.Warn($"{LogTime()} Warn: {Message}");
            }
        }
    }
}