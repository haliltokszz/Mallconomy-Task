using Entities.Concretes;

namespace DataAccess.Abstracts;

public interface IPointsDal
{
    Task<List<Points>> GetPoints();
    Task<List<Points>> GetPointsByUserId(string userId);
    Task<List<Points>> GetPointsByUserId(string userId, int pageNumber, int pageSize);
}