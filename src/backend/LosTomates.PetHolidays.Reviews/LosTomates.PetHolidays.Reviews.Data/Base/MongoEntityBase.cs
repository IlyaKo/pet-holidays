using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LosTomates.PetHolidays.Reviews.Data.Base;

public abstract class MongoEntityBase
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
}
