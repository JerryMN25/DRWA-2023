using BookStoreApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookStoreApi.Services;

public class MapelService
{
    private readonly IMongoCollection<Mapel> _mapelCollection;

    public MapelService(
        IOptions<MapelDatabaseSettings> MapelDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            MapelDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            MapelDatabaseSettings.Value.DatabaseName);

        _mapelCollection = mongoDatabase.GetCollection<Mapel>(
            MapelDatabaseSettings.Value.MapelCollectionName);
    }

    public async Task<List<Mapel>> GetAsync() =>
        await _mapelCollection.Find(_ => true).ToListAsync();

    public async Task<Mapel?> GetAsync(string id) =>
        await _mapelCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Mapel newMapel) =>
        await _mapelCollection.InsertOneAsync(newMapel);

    public async Task UpdateAsync(string id, Mapel updatedMapel) =>
        await _mapelCollection.ReplaceOneAsync(x => x.Id == id, updatedMapel);

    public async Task RemoveAsync(string id) =>
        await _mapelCollection.DeleteOneAsync(x => x.Id == id);
}