using Entities.Concretes;

namespace Business.Abstract;

public interface IUserRankService
{
    Task<UserRanks> GetById(string id);
    Task<List<UserRanks>> GetByMonth(int month);
    Task<List<UserRanks>> GetByUserId(string userId);
    Task<List<UserRanks>> GetAll(int? page = null, int? pageSize = null);
    Task AddMany(IEnumerable<UserRanks> userRanks);
}