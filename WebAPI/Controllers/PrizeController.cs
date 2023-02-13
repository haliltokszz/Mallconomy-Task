using Business.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PrizeController : ControllerBase
{
    private readonly PrizesService _prizeService;

    public PrizeController(PrizesService prizeService)
    {
        _prizeService = prizeService;
    }
    
    [HttpGet("getall")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _prizeService.GetAll();
        if (result.Count > 0)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
    
    [HttpGet("getbyid/{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await _prizeService.GetById(id);
        if (result != null)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
    
    [HttpGet("getbyuserid/{userId}")]
    public async Task<IActionResult> GetByUserId(string userId)
    {
        var result = await _prizeService.GetByUserId(userId);
        if (result != null)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
    
    [HttpGet("getbymonth/{month}")]
    public async Task<IActionResult> GetByMonth(int month)
    {
        var result = await _prizeService.GetByMonth(month);
        if (result != null)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
}