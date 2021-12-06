namespace StockWatch.Configuration
{
    public class SecretProcessor : ISecretProcessor
    {
        private SecretsDataModel secrets;
        private ISecretLoader[] loaders;
        public SecretProcessor(SecretsDataModel secrets, ISecretLoader[] loaders)
        {
            this.secrets = secrets;
            this.loaders = loaders;
        }

        public void LoadSecrets()
        {
            foreach (ISecretLoader loader in loaders)
            {
                loader.Load();
            }

        }
    }
}