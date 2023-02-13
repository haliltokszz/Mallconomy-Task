using Core.DataAccess.MongoDB;
using Core.Utilities;
using DataAccess.Abstracts;
using Entities.Concretes;
using Microsoft.Extensions.Options;

namespace DataAccess.Concretes;

public class LeaderboardDal: MongoDbRepositoryBase<Leaderboard>, ILeaderboardDal
{
    public LeaderboardDal(IOptions<MongoDbSettings> options) : base(options)
    { }
}