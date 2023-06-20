using BookStoreApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookStoreApi.Services;

public class PresensiMengajarService
{
    private readonly IMongoCollection<PresensiMengajar> _presensiMengajarCollection;

    public PresensiMengajarService(
        IOptions<PresensiMengajarDatabaseSettings> PresensiMengajarDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            PresensiMengajarDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            PresensiMengajarDatabaseSettings.Value.DatabaseName);

        _presensiMengajarCollection = mongoDatabase.GetCollection<PresensiMengajar>(
            PresensiMengajarDatabaseSettings.Value.PresensiMengajarCollectionName);
    }

    public async Task<List<PresensiMengajar>> GetAsync() =>
        await _presensiMengajarCollection.Find(_ => true).ToListAsync();

    public async Task<PresensiMengajar?> GetAsync(string id) =>
        await _presensiMengajarCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(PresensiMengajar newPresensiMengajar) =>
        await _presensiMengajarCollection.InsertOneAsync(newPresensiMengajar);

    public async Task UpdateAsync(string id, PresensiMengajar updatedPresensiMengajar) =>
        await _presensiMengajarCollection.ReplaceOneAsync(x => x.Id == id, updatedPresensiMengajar);

    public async Task RemoveAsync(string id) =>
        await _presensiMengajarCollection.DeleteOneAsync(x => x.Id == id);
}