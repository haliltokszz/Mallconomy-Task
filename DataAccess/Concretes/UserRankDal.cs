using Core.DataAccess.MongoDB;
using Core.Utilities;
using DataAccess.Abstracts;
using Entities.Concretes;
using Microsoft.Extensions.Options;

namespace DataAccess.Concretes;

public class UserRankDal: MongoDbRepositoryBase<UserRanks>, IUserRankDal
{
    public UserRankDal(IOptions<MongoDbSettings> options) : base(options)
    {
    }
}