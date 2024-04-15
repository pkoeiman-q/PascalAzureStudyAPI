using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using System;

namespace PascalAzureStudyAPI.Services
{
    public class KeyVaultService : IKeyVaultService
    {
        private readonly IConfiguration _config;
        private string _keyVaultName;
        private string _keyVaultUri;
        private readonly string userAssignedClientId = "474cae16-c137-4ca8-ab82-2b372be900d8";
        private SecretClient _secretClient;

        // Environment variable names
        private string CosmosDbSecretEnvName = "CosmosDbSecret";

        public KeyVaultService(IConfiguration config)
        {
            _config = config;
            _keyVaultName = _config.GetValue<string>("KeyVault:Name");
            _keyVaultUri = "https://" + _keyVaultName + ".vault.azure.net";

            var credential = new DefaultAzureCredential(new DefaultAzureCredentialOptions { ManagedIdentityClientId = userAssignedClientId });
            _secretClient = new SecretClient(new Uri(_keyVaultUri), credential);

            // Update environment variables
            if (Environment.GetEnvironmentVariable(CosmosDbSecretEnvName) == null)
            {
                var secretObject = _secretClient.GetSecret("azure-cosmos-db-key");
                var secretString = secretObject.Value.Value.ToString();
                Environment.SetEnvironmentVariable(CosmosDbSecretEnvName, secretString);
            }
        }

        public string? GetCosmosDbSecret()
        {
            return Environment.GetEnvironmentVariable(CosmosDbSecretEnvName);
        }
    }
}
