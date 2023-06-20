namespace BookStoreApi.Models;

public class PresensiMengajarDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string PresensiMengajarCollectionName { get; set; } = null!;
}