using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using StockWatch.Assets;
using StockWatch.Configuration;

namespace StockWatch.Data
{
    public class CosmosDatabaseProvider : IDatabaseProvider
    {
        private SecretsDataModel secretInfo;
        private CosmosClient cosmosClient;
        private Database database;
        private Container container;
        public CosmosDatabaseProvider(SecretsDataModel secrets)
        {
            this.secretInfo = secrets;

        }
        // new CosmosClientOptions(){
        //     SerializerOptions = new CosmosSerializationOptions(){
        //       PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
        //     }
        public async Task ConnectToDatabase()
        {

            this.cosmosClient = new CosmosClient(
            secretInfo.CosmosDbConnData.EndPointUri,
            secretInfo.CosmosDbConnData.PrimaryKey,
            new CosmosClientOptions()
            {
                ApplicationName = "StockWatch",
                SerializerOptions = new CosmosSerializationOptions
                {
                    PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
                }
            });

            this.database = await this.cosmosClient.CreateDatabaseIfNotExistsAsync(
                secretInfo.CosmosDbConnData.DatabaseId
            );
            this.container = await this.database.CreateContainerIfNotExistsAsync(
                secretInfo.CosmosDbConnData.ContainerId,
                secretInfo.CosmosDbConnData.ContainerKey,
                secretInfo.CosmosDbConnData.Throughput);

        }

        public async Task<Dictionary<string, AssetHistoryModel>> GetHistory(List<AssetModel> assets)
        {
            Dictionary<string, AssetHistoryModel> assetHistories = new Dictionary<string, AssetHistoryModel>();
            foreach (AssetModel asset in assets)
            {
                if (assetHistories.ContainsKey(asset.Symbol))
                {
                    continue;
                }
                assetHistories.Add(asset.Symbol, new AssetHistoryModel());
                assetHistories[asset.Symbol].HistoryEntries = new List<AssetModel>();
                // pull history from db
                QueryDefinition queryDefinition = new QueryDefinition("SELECT * FROM c WHERE c.symbol =  @symbol")
                    .WithParameter("@symbol", asset.Symbol);

                FeedIterator<AssetModel> queryResultSetIterator = this.container.GetItemQueryIterator<AssetModel>(queryDefinition);

                while (queryResultSetIterator.HasMoreResults)
                {
                    FeedResponse<AssetModel> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                    foreach (AssetModel assetInDb in currentResultSet)
                    {
                        assetHistories[asset.Symbol].HistoryEntries.Add(assetInDb);
                        if(DateTime.Compare(assetHistories[asset.Symbol].LastEntry, assetInDb.ReportDate) < 0)
                        {
                            assetHistories[asset.Symbol].LastEntry = assetInDb.ReportDate;
                        }
                    }
                }
            }
            return assetHistories;
        }

        public async Task SaveHistory(List<AssetModel> assets)
        {
            foreach (AssetModel asset in assets)
            {
                await this.container.CreateItemAsync<AssetModel>(asset);
            }
        }
    }
}