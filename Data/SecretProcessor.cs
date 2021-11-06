namespace StockWatch.Data
{
    public class SecretProcessor : ISecretProcessor
    {
        private SecretData secrets;
        private ISecretLoader[] loaders;
        public SecretProcessor(SecretData secrets, ISecretLoader[] loaders)
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