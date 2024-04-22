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

        //[HttpGet]
        //public async Task<IActionResult> Get()
        //{
        //    var id = Guid.NewGuid().ToString();
        //    var portfolioId = Guid.NewGuid().ToString();
        //    var workExp = new WorkExperience()
        //    {
        //        Title = "Test",
        //        Description = "Test",
        //        Date = DateTime.Now,
        //    };

        //    var portfolio = new Portfolio()
        //    {
        //        Id = id,
        //        PortfolioId = portfolioId,
        //        Experiences = [workExp]
        //    };
        //    await _portfoliosService.AddAsync(portfolio);
        //    var result = await _portfoliosService.GetAsync(portfolioId);
        //    return Ok(result);
        //}

        [HttpGet("{portfolioId}")]
        public async Task<IActionResult> GetPortfolio(string portfolioId)
        {
            Portfolio portfolio = await _portfoliosService.GetAsync(portfolioId);
            if (portfolio == null) return StatusCode(500, "The requested portfolio has not been found.");
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
            if (portfolio == null) return StatusCode(500, "The requested portfolio has not been found.");

            portfolio.Experiences = experiences;
            await _portfoliosService.UpdateAsync(portfolioId, portfolio);

            return Ok(portfolio);
        }

        /*
        public async Task<IActionResult> DeletePortfolio()
        {
            return Ok("");
        }

        public async Task<IActionResult> GetPortfolio()
        {
            return Ok("");
        }*/
    }
}
