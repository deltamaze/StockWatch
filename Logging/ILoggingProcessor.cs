namespace StockWatch.Logging
{
    public interface ILoggingProcessor
    {
        void Debug(string Message);
        void Error(string Message);
        void Info(string Message);
        void Trace(string Message);
        void Warn(string Message);
    }
}