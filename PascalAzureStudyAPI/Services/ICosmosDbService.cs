using PascalAzureStudyAPI.Models;

namespace PascalAzureStudyAPI.Services
{
    public interface ICosmosDbService
    {
        Task AddAsync(Portfolio portfolio);
        Task DeleteAsync(string id);
        Task<Portfolio> GetAsync(string id);
        Task<IEnumerable<Portfolio>> GetMultipleAsync(string queryString);
        Task UpdateAsync(string id, Portfolio portfolio);
    }
}