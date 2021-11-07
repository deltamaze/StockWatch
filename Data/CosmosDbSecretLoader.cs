using System.IO;
using System.Text.Json;

namespace StockWatch.Data
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
            string secretFile = "cosmosConn.json";
            if(File.Exists(secretFile)){
                this.secrets.CosmosDbConnData = JsonSerializer.Deserialize<CosmosDbConnData>(secretFile);
            }
        }
    }
    
}