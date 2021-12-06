using System.IO;
using System.Text.Json;
using StockWatch.Data;

namespace StockWatch.Configuration
{
    public class CosmosDbSecretLoader : ISecretLoader
    {
        private SecretsDataModel secrets;
        public CosmosDbSecretLoader(SecretsDataModel secrets)
        {
            this.secrets = secrets;
        }
        public void Load()
        {
            //this file is a gitignored folder, you'll have to make your own
            string secretFile = @".\secrets\cosmosConn.json";
            if(File.Exists(secretFile)){
                string fileContent = File.ReadAllText(secretFile);
                this.secrets.CosmosDbConnData = JsonSerializer.Deserialize<CosmosDbConnData>(fileContent);
            }
        }
    }
    
}