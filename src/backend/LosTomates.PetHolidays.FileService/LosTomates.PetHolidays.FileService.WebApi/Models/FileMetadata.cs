using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LosTomates.PetHolidays.FileService.WebApi.Models;

public class FileMetadata
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

    [BsonElement("entityId")]
    public string EntityId { get; set; } = string.Empty;

    [BsonElement("url")]
    public string Url { get; set; } = string.Empty;

    [BsonElement("bucketName")]
    public string BucketName { get; set; } = string.Empty;

    [BsonElement("uploadedAt")]
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
}
