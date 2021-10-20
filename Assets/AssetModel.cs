namespace StockWatch.Assets
{
    public class AssetModel
    {
        public string Company { get; set; }
        public string Symbol { get; set; }
        public string Url { get; set; }
        public decimal MarketCap { get; set; }
        public decimal PercentChange { get; set; }
        public decimal AvgVolume { get; set; }
        public decimal UnitPrice { get; set; }
    }
    
}