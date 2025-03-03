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

    [BsonElement("objectName")]
    public string ObjectName { get; set; } = string.Empty;

    [BsonElement("bucketName")]
    public string BucketName { get; set; } = string.Empty;

    [BsonElement("uploadedAt")]
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

    [BsonElement("originalFileName")]
    public string OriginalFileName { get; set; } = string.Empty;

    [BsonElement("fileExtension")]
    public string FileExtension { get; set; } = string.Empty;

    [BsonElement("fileSize")]
    public long FileSize { get; set; } = 0;

    [BsonElement("contentType")]
    public string ContentType { get; set; } = "application/octet-stream";
}
