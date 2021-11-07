using Microsoft.Azure.Cosmos;
using System.Collections.Generic;
using System.Threading.Tasks;
using StockWatch.Assets;
namespace StockWatch.Data
{
    public class CosmosDatabaseProvider : IDatabaseProvider
    {
        private CosmosDbConnData connInfo;
        private CosmosClient cosmosClient;
        private Database database;
        private Container container;
        public CosmosDatabaseProvider(SecretsDataModel secrets)
        {
            this.connInfo = secrets.CosmosDbConnData;
            this.cosmosClient = new CosmosClient(
                connInfo.EndpointUri,
                connInfo.PrimaryKey,
                new CosmosClientOptions() { ApplicationName = "StockWatch" });
        }

        public async Task ConnectToDatabase()
        {
            this.database = await this.cosmosClient.CreateDatabaseIfNotExistsAsync(
                connInfo.DatabaseId);
            this.container = await this.database.CreateContainerIfNotExistsAsync(
                connInfo.ContainerId,
                connInfo.ContainerKey,
                connInfo.Throughput);
            
        }

        public async Task<Dictionary<string,AssetHistoryModel>> GetHistory(List<AssetModel> assets)
        {
            Dictionary<string,AssetHistoryModel> assetHistories = new Dictionary<string, AssetHistoryModel>();
            foreach (AssetModel asset in assets)
            {
                if(assetHistories[asset.Symbol] != null){
                    continue;
                }
                assetHistories[asset.Symbol] = new AssetHistoryModel();
                // pull history from db
                
                //PICK UP FROM HERE
                //Look into parameterized comsos db input
                
                // var sqlQueryText = "SELECT * FROM c WHERE c.Symbol = 'Andersen'";

                // QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
                // FeedIterator<Family> queryResultSetIterator = this.container.GetItemQueryIterator<Family>(queryDefinition);

                // List<Family> families = new List<Family>();

                // while (queryResultSetIterator.HasMoreResults)
                // {
                //     FeedResponse<Family> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                //     foreach (Family family in currentResultSet)
                //     {
                //         families.Add(family);
                //         Console.WriteLine("\tRead {0}\n", family);
                //     }
                // }

            }
            return assetHistories;
        }

        public async Task SaveHistory(List<AssetModel> asset)
        {
            throw new System.Exception("Not Implemented");
        }
    }
}