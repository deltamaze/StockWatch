using System.IO;
using System.Text.Json;
using StockWatch.Data;

namespace StockWatch.Configuration
{
    public class TwitterSecretLoader : ISecretLoader
    {
        private SecretsDataModel secrets;
        public TwitterSecretLoader(SecretsDataModel secrets)
        {
            this.secrets = secrets;
        }
        public void Load()
        {
            //this file is a gitignored folder, you'll have to make your own
            string secretFile = @".\secrets\twitterConn.json";
            if(File.Exists(secretFile)){
                string fileContent = File.ReadAllText(secretFile);
                this.secrets.TwitterConnData = JsonSerializer.Deserialize<TwitterConnData>(fileContent);
            }
        }
    }
    
}