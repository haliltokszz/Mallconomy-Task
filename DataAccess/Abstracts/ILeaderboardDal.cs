using Core.DataAccess;
using Entities.Concretes;

namespace DataAccess.Abstracts;

public interface ILeaderboardDal: IRepository<Leaderboard, string>
{
    
}