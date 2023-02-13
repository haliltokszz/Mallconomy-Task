using Business.Abstract;
using DataAccess.Abstracts;
using Entities.Concretes;

namespace Business.Concrete;

public class LeaderboardService: ILeaderboardService
{
    private readonly ILeaderboardDal _leaderboardRepository;
    private readonly IPointsDal _pointsRepository;
    private readonly IUserRankService _userRankService;

    public LeaderboardService(ILeaderboardDal leaderboardRepository, IPointsDal pointsRepository, IUserRankService userRankService)
    {
        _leaderboardRepository = leaderboardRepository;
        _pointsRepository = pointsRepository;
        _userRankService = userRankService;
    }

    public Task<Leaderboard> GetById(string id)
    {
        return _leaderboardRepository.GetByIdAsync(id);
    }

    public Task<List<Leaderboard>> GetAll(int? page = null, int? pageSize = null)
    {
        return _leaderboardRepository.GetAllWithPaginationAsync(x=>true ,page, pageSize);
    }

    public async Task<Leaderboard> CreateMonthlyLeaderboard(int month)
    {
        //Kullanıcılara ödülleri dağıt

        var currentMonthLeaderboard = await _leaderboardRepository.GetAsync(x => x.Month == month);
        if (currentMonthLeaderboard != null)
        {
            throw new Exception("The Leaderboard for this month has already been generated.");
        }
        
        var points = await _pointsRepository.GetPoints();

        // Sadece onaylanmış Point değerlerini filtrele
        var filteredPoints = points.Where(p => p.Approved);
        var leaderboard = new Leaderboard
        {
            Month = month,
            UserRanks = points.GroupBy(x => x.UserId)
                .Select((g, index) => new UserRanks
                {
                    UserId = g.Key.ToString(),
                    TotalPoints = g.Sum(x => x.Point),
                    Rank = index + 1,
                    Month = month,
                })
                .OrderByDescending(x => x.TotalPoints)
                .ToArray()
        };

        try
        {
            await _leaderboardRepository.AddAsync(leaderboard);
            await _userRankService.AddMany(leaderboard.UserRanks);
        }
        catch (Exception e)
        {
            throw new Exception("Error while creating leaderboard.", e);
        }

        return leaderboard;
    }
}