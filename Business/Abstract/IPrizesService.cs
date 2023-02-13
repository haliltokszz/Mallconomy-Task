using Entities.Concretes;

namespace Business.Abstract;

public interface IPrizesService
{
    Task<Prize> GetById(string id);
    Task<List<Prize>> GetAll(int? page = null, int? pageSize = null);
    Task<List<Prize>> GetByMonth(int month);
    Task<List<Prize>> GetByUserId(string userId);
    Task<List<Prize>> AwardPrizes(IEnumerable<UserRanks> userRanks);
}