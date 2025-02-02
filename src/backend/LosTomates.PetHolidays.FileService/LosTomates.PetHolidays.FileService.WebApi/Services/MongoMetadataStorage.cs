using LosTomates.PetHolidays.FileService.WebApi.Configuration;
using LosTomates.PetHolidays.FileService.WebApi.Models;
using LosTomates.PetHolidays.FileService.WebApi.Services.Abstractions;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LosTomates.PetHolidays.FileService.WebApi.Services;

public class MongoMetadataStorage : IMetadataStorage
{
    private readonly IMongoDatabase _database;

    public MongoMetadataStorage(IOptions<MongoDbSettings> options)
    {
        var settings = options.Value;
        var client = new MongoClient(settings.ConnectionString);
        _database = client.GetDatabase(settings.DatabaseName);
    }

    public async Task InsertMetadataAsync(FileMetadata metadata, string collectionName)
    {
        var collection = GetCollection(collectionName);
        await collection.DeleteManyAsync(x => x.EntityId == metadata.EntityId);
        await collection.InsertOneAsync(metadata);
    }

    public async Task<FileMetadata?> FindFileByEntityAsync(string entityId, string collectionName)
    {
        var collection = GetCollection(collectionName);
        return await collection.Find(x => x.EntityId == entityId).FirstOrDefaultAsync();
    }

    public async Task DeleteFileByEntityAsync(string entityId, string collectionName)
    {
        var collection = GetCollection(collectionName);
        await collection.DeleteOneAsync(x => x.EntityId == entityId);
    }

    private IMongoCollection<FileMetadata> GetCollection(string collectionName)
    {
        return _database.GetCollection<FileMetadata>(collectionName);
    }
}
