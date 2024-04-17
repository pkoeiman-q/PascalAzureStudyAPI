using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PascalAzureStudyAPI.Models;
using PascalAzureStudyAPI.Services;

namespace PascalAzureStudyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfoliosController : ControllerBase
    {
        private readonly IPortfoliosService _portfoliosService;
        public PortfoliosController(IPortfoliosService portfoliosService)
        {
            _portfoliosService = portfoliosService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var id = Guid.NewGuid().ToString();
            var portfolio = new Portfolio()
            {
                Id = id,
                PortfolioId = id,
                UserId = "0",
                Description = "Lorum ipsum"
            };
            await _portfoliosService.AddAsync(portfolio);
            var result = await _portfoliosService.GetAsync(id);
            return Ok(result);
        }
    }
}
