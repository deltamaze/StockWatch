
namespace StockWatch.Data
{
    public class CosmosDbConnData
    {
        // COSMOS DB Connection info
        public string EndPointUri { get; set; }
        public string PrimaryKey { get; set; }
        public string DatabaseId { get; set; }
        public string ContainerId { get; set; }
        public string ContainerKey { get; set; }
        public int Throughput { get; set; }
    }

}