using Business.Abstract;
using DataAccess.Abstracts;
using Entities.Concretes;

namespace Business.Concrete;

public class PrizesService: IPrizesService
{
    private readonly IPrizeDal _prizesRepository;

    public PrizesService(IPrizeDal prizesRepository)
    {
        _prizesRepository = prizesRepository;
    }

    public Task<Prize> GetById(string id)
    {
        return _prizesRepository.GetByIdAsync(id);
    }

    public Task<List<Prize>> GetAll(int? page = null, int? pageSize = null)
    {
        return _prizesRepository.GetAllWithPaginationAsync(x=>true ,page, pageSize);
    }

    public Task<List<Prize>> GetByMonth(int month)
    {
        return _prizesRepository.GetAllWithPaginationAsync(x => x.Month == month);
    }

    public Task<List<Prize>> GetByUserId(string userId)
    {
        return _prizesRepository.GetAllWithPaginationAsync(x => x.UserId == userId);
    }

    public async Task<List<Prize>> AwardPrizes(IEnumerable<UserRanks> userRanks)
    {
        if (userRanks == null || !userRanks.Any())
        {
            throw new ArgumentException("User ranks cannot be null or empty.");
        }

        var prizes = userRanks.Select(u => MapToPrizes(u));

        await _prizesRepository.AddRangeAsync(prizes);

        return prizes.ToList();
    }
    
    private Prize MapToPrizes(UserRanks userRank)
    {
        var prize = new Prize
        {
            UserId = userRank.UserId,
            Month = userRank.Month,
        };

        switch (userRank.Rank)
        {
            case 1:
                prize.Name = "First Prize";
                prize.Amount = 500;
                break;
            case 2:
                prize.Name = "Second Prize";
                prize.Amount = 250;
                break;
            case 3:
                prize.Name = "Third Prize";
                prize.Amount = 100;
                break;
            case <= 100:
                prize.Name = "Top100";
                prize.Amount = 25;
                break;
            case <= 1000:
                prize.Name = "Consolation Prize";
                prize.Amount = (12500 / 1000);
                break;
        }
        return prize;
    }
}