using Core.DataAccess;
using Entities.Concretes;

namespace DataAccess.Abstracts;

public interface IPrizeDal : IRepository<Prize, string>
{
    
}