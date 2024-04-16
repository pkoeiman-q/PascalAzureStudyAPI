using Microsoft.Azure.Cosmos;
using PascalAzureStudyAPI.Models;
using PascalAzureStudyAPI.Repositories;

namespace PascalAzureStudyAPI.Services
{
    public class PortfoliosService : IPortfoliosService
    {
        private readonly IPortfoliosRepository _repository;

        public PortfoliosService(IPortfoliosRepository repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(Portfolio portfolio) => await _repository.AddAsync(portfolio);

        public async Task DeleteAsync(string id) => await _repository.DeleteAsync(id);

        public async Task<Portfolio> GetAsync(string id) => await _repository.GetAsync(id);

        public async Task<IEnumerable<Portfolio>> GetMultipleAsync(string queryString) => await _repository.GetMultipleAsync(queryString);

        public async Task UpdateAsync(string id, Portfolio portfolio) => await _repository.UpdateAsync(id, portfolio);
    }
}
