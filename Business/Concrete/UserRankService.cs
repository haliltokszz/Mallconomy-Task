using Business.Abstract;
using DataAccess.Abstracts;
using Entities.Concretes;

namespace Business.Concrete;

public class UserRankService: IUserRankService
{
    private readonly IUserRankDal _userRankRepository;

    public UserRankService(IUserRankDal userRankDal)
    {
        _userRankRepository = userRankDal;
    }

    public Task<UserRanks> GetById(string id)
    {
        return _userRankRepository.GetByIdAsync(id);
    }

    public Task<List<UserRanks>> GetByMonth(int month)
    {
        return _userRankRepository.GetAllWithPaginationAsync(x => x.Month == month);
    }

    public Task<List<UserRanks>> GetByUserId(string userId)
    {
        return _userRankRepository.GetAllWithPaginationAsync(x => x.UserId == userId);
    }

    public Task<List<UserRanks>> GetAll(int? page = null, int? pageSize = null)
    {
        return _userRankRepository.GetAllWithPaginationAsync(x=>true ,page, pageSize);
    }

    public async Task AddMany(IEnumerable<UserRanks> userRanks)
    {
        await _userRankRepository.AddRangeAsync(userRanks);
    }
}