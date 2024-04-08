using Microsoft.Azure.Cosmos;
using PascalAzureStudyAPI.Models;

namespace PascalAzureStudyAPI.Services
{
    public class CosmosDbService : ICosmosDbService
    {
        private Container _container;

        public CosmosDbService(
            CosmosClient cosmosDbClient,
            string databaseName,
            string containerName)
        {
            _container = cosmosDbClient.GetContainer(databaseName, containerName);
        }

        public async Task AddAsync(Portfolio portfolio)
        {
            await _container.CreateItemAsync(portfolio, new PartitionKey(portfolio.PortfolioId));
        }

        public async Task DeleteAsync(string id)
        {
            await _container.DeleteItemAsync<Portfolio>(id, new PartitionKey(id));
        }

        public async Task<Portfolio> GetAsync(string id)
        {
            try
            {
                var response = await _container.ReadItemAsync<Portfolio>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException) //For handling item not found and other exceptions
            {
                return null;
            }
        }

        public async Task<IEnumerable<Portfolio>> GetMultipleAsync(string queryString)
        {
            var query = _container.GetItemQueryIterator<Portfolio>(new QueryDefinition(queryString));

            var results = new List<Portfolio>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task UpdateAsync(string id, Portfolio portfolio)
        {
            await _container.UpsertItemAsync(portfolio, new PartitionKey(id));
        }
    }
}
