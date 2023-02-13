using Entities.Concretes;

namespace Business.Abstract;

public interface ILeaderboardService
{
    Task<Leaderboard> GetById(string id);
    Task<List<Leaderboard>> GetAll(int? page = null, int? pageSize = null);
    Task<Leaderboard> CreateMonthlyLeaderboard(int month);
}