using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Core.Entities;

public abstract class MongoEntity: IEntity<string>
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement(Order = 0)]
    public string Id { get; set; }  = ObjectId.GenerateNewId().ToString();

    [BsonRepresentation(BsonType.DateTime)]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    [BsonElement(Order = 101)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 
}