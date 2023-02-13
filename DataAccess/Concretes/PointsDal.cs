using Core.Utilities;
using DataAccess.Abstracts;
using Entities.Concretes;
using MongoDB.Bson.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DataAccess.Concretes;

public class PointsDal: IPointsDal
{
    public async Task<List<Points>> GetPoints()
    {
        var client = new HttpClient();
        var response = await client.GetAsync("https://cdn.mallconomy.com/testcase/points.json");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return BsonSerializer.Deserialize<List<Points>>(content);
    }

    public async Task<List<Points>> GetPointsByUserId(string userId)
    {
        var points = await GetPoints();
        return points.Where(p => p.UserId.ToString() == userId).ToList();
    }

    public async Task<List<Points>> GetPointsByUserId(string userId, int pageNumber, int pageSize)
    {
        var points = await GetPoints();
        var filteredPoints = points.Where(p => p.UserId.ToString() == userId).ToList();
        return filteredPoints.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
    }
}