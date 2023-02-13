using Core.DataAccess.MongoDB;
using Core.Utilities;
using DataAccess.Abstracts;
using Entities.Concretes;
using Microsoft.Extensions.Options;

namespace DataAccess.Concretes;

public class PrizeDal: MongoDbRepositoryBase<Prize>, IPrizeDal
{
    public PrizeDal(IOptions<MongoDbSettings> options) : base(options)
    {
    }
}