using System.IO;
using System.Text.Json;
using StockWatch.Data;
using StockWatch.Logging;

namespace StockWatch.Configuration
{
    public class CosmosDbSecretLoader : ISecretLoader
    {
        private SecretsDataModel secrets;
        private ILoggingProcessor logger;
        public CosmosDbSecretLoader(SecretsDataModel secrets, ILoggingProcessor logger)
        {
            this.secrets = secrets;
            this.logger = logger;
        }
        public void Load()
        {
            //this file is a gitignored folder, you'll have to make your own
            string secretFile = @".\secrets\cosmos.json";
            if(File.Exists(secretFile)){
                string fileContent = File.ReadAllText(secretFile);
                this.secrets.CosmosDbConnData = JsonSerializer.Deserialize<CosmosDbConnData>(fileContent);
            }
            logger.Warn("No cosmos.json file found, unable to load db connection info");
        }
    }
    
}