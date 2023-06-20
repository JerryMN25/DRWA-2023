namespace BookStoreApi.Models;

public class GuruDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string GuruCollectionName { get; set; } = null!;
}