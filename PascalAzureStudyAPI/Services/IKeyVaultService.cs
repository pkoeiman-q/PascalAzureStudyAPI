namespace PascalAzureStudyAPI.Services
{
    public interface IKeyVaultService
    {
        string? GetCosmosDbPrimaryKey();
    }
}