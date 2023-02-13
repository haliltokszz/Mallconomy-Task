using Core.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Entities.Concretes;

public class Leaderboard : MongoEntity
{
    [BsonElement("month")]
    public int Month { get; set; }

    [BsonElement("user_ranks")]
    public IEnumerable<UserRanks> UserRanks { get; set; }
}