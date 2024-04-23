using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PascalAzureStudyAPI.Models;
using PascalAzureStudyAPI.Services;
using System.Numerics;

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

        private ObjectResult PortfolioNotFound()
        {
            return StatusCode(500, "The requested portfolio has not been found.");
        }

        [HttpGet("{portfolioId}")]
        public async Task<IActionResult> GetPortfolio(string portfolioId)
        {
            Portfolio portfolio = await _portfoliosService.GetAsync(portfolioId);
            if (portfolio == null) return PortfolioNotFound();

            return Ok(portfolio);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePortfolio(string portfolioId)
        {
            var portfolio = new Portfolio()
            {
                Id = portfolioId,
                PortfolioId = portfolioId,
                Experiences = []
            };

            await _portfoliosService.AddAsync(portfolio);
            return Ok(portfolio);
        }

        [HttpPut("{portfolioId}")]
        public async Task<IActionResult> UpdatePortfolioWithExperience(string portfolioId, [FromBody] IEnumerable<WorkExperience> experiences)
        {
            Portfolio portfolio = await _portfoliosService.GetAsync(portfolioId);
            if (portfolio == null) return PortfolioNotFound();

            portfolio.Experiences = experiences;
            await _portfoliosService.UpdateAsync(portfolioId, portfolio);

            return Ok(portfolio);
        }

        [HttpDelete("{portfolioId}")]
        public async Task<IActionResult> DeletePortfolio(string portfolioId)
        {
            Portfolio portfolio = await _portfoliosService.GetAsync(portfolioId);
            if (portfolio == null) return PortfolioNotFound();

            await _portfoliosService.DeleteAsync(portfolioId);
            return Ok(portfolio);
        }
    }
}
