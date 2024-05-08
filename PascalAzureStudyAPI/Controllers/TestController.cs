using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Mvc;
using PascalAzureStudyAPI.Services;
using System.Diagnostics;

namespace PascalAzureStudyAPI.Controllers
{
    public class TestController : Controller
    {
        private readonly ILogger<TestController> _logger;
        private readonly IKeyVaultService _keyVaultService;

        public TestController(ILogger<TestController> logger, IKeyVaultService keyVaultService)
        {
            _logger = logger;
            _keyVaultService = keyVaultService;
        }

        [Route("/")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok("API is live! Test.");
        }
    }
}
