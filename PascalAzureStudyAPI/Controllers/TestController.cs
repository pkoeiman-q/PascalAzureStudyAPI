using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace PascalAzureStudyAPI.Controllers
{
    public class TestController : Controller
    {
        private readonly ILogger<TestController> _logger;
        private readonly IConfiguration _config;

        public TestController(ILogger<TestController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        [Route("/")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok("API is live!");
        }

        [Route("yello")]
        [HttpGet]
        public async Task<IActionResult> Yello()
        {
            Console.WriteLine("Test console");
            Debug.WriteLine("Test debug");
            _logger.LogInformation("Test logger");
            string keyVaultName = _config.GetValue<string>("KeyVault:Name");
            Console.WriteLine("GOTTEN KEYVAULT NAME");
            var kvUri = "https://" + keyVaultName + ".vault.azure.net";

            string userAssignedClientId = "474cae16-c137-4ca8-ab82-2b372be900d8";
            var credential = new DefaultAzureCredential(new DefaultAzureCredentialOptions { ManagedIdentityClientId = userAssignedClientId });
            var client = new SecretClient(new Uri(kvUri), credential);

            try
            {
                var secret = await client.GetSecretAsync("azure-cosmos-db-key");
                return Ok(secret);
            }
            catch (Exception ex)
            {
                var requestObj = new ObjectResult (new
                {
                    credential = credential,
                    message = ex.Message,
                });
                return BadRequest(requestObj);
            }
        }
    }
}
