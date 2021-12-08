using StockWatch.Data;

namespace StockWatch.Configuration
{
    public class SecretsDataModel
    {
        public CosmosDbConnData CosmosDbConnData { get; set; }
        public TwitterConnData TwitterConnData { get; set; }
    }

}