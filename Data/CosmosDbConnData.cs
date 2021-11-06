
namespace StockWatch.Data
{
    public class CosmosDbConnData
    {
        // COSMOS DB Connection info
        public string EndpointUri { get; set; }
        public string PrimaryKey { get; set; }
        public string DatabaseId { get; set; }
        public string ContainerId { get; set; }
    }

}