using PascalAzureStudyAPI.Models;

namespace PascalAzureStudyAPI.Repositories
{
    public interface IPortfoliosRepository
    {
        Task AddAsync(Portfolio portfolio);
        Task DeleteAsync(string id);
        Task<Portfolio> GetAsync(string id);
        Task<IEnumerable<Portfolio>> GetMultipleAsync(string queryString);
        Task UpdateAsync(string id, Portfolio portfolio);
    }
}