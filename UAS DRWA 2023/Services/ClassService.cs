using BookStoreApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookStoreApi.Services;

public class ClassService
{
    private readonly IMongoCollection<Class> _classCollection;

    public ClassService(
        IOptions<ClassDatabaseSettings> classDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            classDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            classDatabaseSettings.Value.DatabaseName);

        _classCollection = mongoDatabase.GetCollection<Class>(
            classDatabaseSettings.Value.ClassCollectionName);
    }

    public async Task<List<Class>> GetAsync() =>
        await _classCollection.Find(_ => true).ToListAsync();

    public async Task<Class?> GetAsync(string id) =>
        await _classCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Class newClass) =>
        await _classCollection.InsertOneAsync(newClass);

    public async Task UpdateAsync(string id, Class updatedClass) =>
        await _classCollection.ReplaceOneAsync(x => x.Id == id, updatedClass);

    public async Task RemoveAsync(string id) =>
        await _classCollection.DeleteOneAsync(x => x.Id == id);
}