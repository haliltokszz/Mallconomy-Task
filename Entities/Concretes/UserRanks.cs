using Core.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Entities.Concretes;

public class UserRanks: MongoEntity
{
    [BsonElement("user_id")]
    public string UserId { get; set; }

    [BsonElement("month")]
    public int Month { get; set; }
    
    [BsonElement("rank")]
    public int Rank { get; set; }
    
    [BsonElement("total_points")]
    public int TotalPoints { get; set; }
}