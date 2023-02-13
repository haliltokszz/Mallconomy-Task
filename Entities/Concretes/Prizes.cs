using Core.Entities;
using MongoDB.Bson.Serialization.Attributes;

namespace Entities.Concretes;

public class Prize : MongoEntity
{
    [BsonElement("user_id")]
    public string UserId { get; set; }

    [BsonElement("name")]
    public string Name { get; set; }

    [BsonElement("amount")]
    public int Amount { get; set; }

    [BsonElement("month")]
    public int Month { get; set; }
}