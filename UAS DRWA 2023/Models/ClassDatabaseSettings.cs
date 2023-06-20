namespace BookStoreApi.Models;

public class ClassDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string ClassCollectionName { get; set; } = null!;
}