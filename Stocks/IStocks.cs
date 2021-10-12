using System.Collections.Generic;

namespace StockWatch.Stocks
{
    public interface IStocks
    {
        IEnumerable<StocksModel> GainingStocks();
        IEnumerable<StocksModel> LosingStocks();
    }
}