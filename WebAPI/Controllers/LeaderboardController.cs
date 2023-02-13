using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LeaderboardController : ControllerBase
{
    private readonly ILeaderboardService _leaderboardService;
    private readonly IPrizesService _prizeService;
    private readonly IUserRankService _userRankService;

    public LeaderboardController(ILeaderboardService leaderboardService, IPrizesService prizeService, IUserRankService userRankService)
    {
        _leaderboardService = leaderboardService;
        _prizeService = prizeService;
        _userRankService = userRankService;
    }
    
    [HttpGet("getall")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _leaderboardService.GetAll();
        if (result.Count > 0)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
    
    [HttpGet("getbyid/{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await _leaderboardService.GetById(id);
        if (result != null)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
    
    [HttpGet("getbymonth/{month}")]
    public async Task<IActionResult> GetByMonth(int month)
    {
        var result = await _userRankService.GetByMonth(month);
        if (result != null)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
    
    [HttpGet("getbyuserid/{userId}")]
    public async Task<IActionResult> GetByUserId(string userId)
    {
        var result = await _userRankService.GetByUserId(userId);
        if (result != null)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
    
    //Create a new leaderboard by month
    [HttpPost("add/{month}")]
    public async Task<IActionResult> Add(int month)
    {
        var leaderboardResult = await _leaderboardService.CreateMonthlyLeaderboard(month);
        await _prizeService.AwardPrizes(leaderboardResult.UserRanks);
        
        if (leaderboardResult != null)
        {
            return Ok(leaderboardResult);
        }
        return BadRequest(leaderboardResult);
    }
}