using LosTomates.PetHolidays.FileService.WebApi.Configuration;
using LosTomates.PetHolidays.FileService.WebApi.Models;
using LosTomates.PetHolidays.FileService.WebApi.Services.Abstractions;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LosTomates.PetHolidays.FileService.WebApi.Services;

public class MongoMetadataStorage : IMetadataStorage
{
    private readonly IMongoDatabase _database;

    public MongoMetadataStorage(IMongoClient mongoClient, IOptions<MongoDbSettings> options)
    {
        var settings = options.Value;
        _database = mongoClient.GetDatabase(settings.DatabaseName);
    }

    public async Task InsertMetadataAsync(FileMetadata metadata, string collectionName)
    {
        var collection = GetCollection(collectionName);
        await collection.InsertOneAsync(metadata);
    }

    public async Task<List<FileMetadata>> FindFilesByEntityIdAsync(string entityId, string collectionName)
    {
        var collection = GetCollection(collectionName);
        return await collection.Find(x => x.EntityId == entityId)
            .ToListAsync();
    }

    public async Task DeleteFileByEntitiesIdAsync(List<string> entitiesId, string collectionName)
    {
        var collection = GetCollection(collectionName);
        await collection.DeleteManyAsync(x => entitiesId.Contains(x.EntityId));
    }


    private IMongoCollection<FileMetadata> GetCollection(string collectionName)
    {
        return _database.GetCollection<FileMetadata>(collectionName);
    }
}
