namespace StockWatch.Logging
{
    public interface ILogger
    {
        void Trace(string msg);
        void Info(string msg);
        void Debug(string msg);
        void Error(string msg);
        void Warn(string msg);
    }
    
}