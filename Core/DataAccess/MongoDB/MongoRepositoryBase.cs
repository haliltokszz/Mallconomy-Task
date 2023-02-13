using System.Linq.Expressions;
using Core.Entities;
using Core.Utilities;
using MongoDB.Driver;
using Microsoft.Extensions.Options;

namespace Core.DataAccess.MongoDB;

public abstract class MongoDbRepositoryBase<T> : IRepository<T, string> where T : MongoEntity, new()
    {
        private readonly IMongoCollection<T> _collection;

        protected MongoDbRepositoryBase(IOptions<MongoDbSettings> options)
        {
            var mongoDbSettings = options.Value;
            var client = new MongoClient(mongoDbSettings.ConnectionString);
            var db = client.GetDatabase(mongoDbSettings.DatabaseName);
            _collection = db.GetCollection<T>(typeof(T).Name.ToLowerInvariant());
        }

        public virtual IQueryable<T> Get(Expression<Func<T, bool>> predicate = null)
        {
            return predicate == null
                ? _collection.AsQueryable()
                : _collection.AsQueryable().Where(predicate);
        }  

        public virtual Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return _collection.Find(predicate).FirstOrDefaultAsync();
        }
        
        public async Task<List<T>> GetAllAsync()
        {
            return await _collection.Find(x => true).ToListAsync();
        }
        public async Task<List<T>> GetAllWithPaginationAsync(Expression<Func<T, bool>> predicate, int? page = null, int? pageSize = null)
        {
            if(page != null && pageSize != null)
                return await _collection.Find(predicate).Skip((page.Value - 1) * pageSize.Value).Limit(pageSize.Value).ToListAsync();
            
            return await _collection.Find(predicate).ToListAsync();
        }

        public virtual Task<T> GetByIdAsync(string id)
        {
            return _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            var options = new InsertOneOptions {BypassDocumentValidation = false};
            await _collection.InsertOneAsync(entity, options);
            return entity;
        }

        public virtual async Task AddRangeAsync(IEnumerable<T> entities)
        {
            // var options = new BulkWriteOptions {IsOrdered = false, BypassDocumentValidation = false};
            // return (await _collection.BulkWriteAsync((IEnumerable<WriteModel<T>>) entities, options)).IsAcknowledged;
            
            var entitiesToInsert = new List<WriteModel<T>>();
            foreach (var entity in entities)
            {
                entitiesToInsert.Add(new InsertOneModel<T>(entity));
            }

            await _collection.BulkWriteAsync(entitiesToInsert);
        }

        public virtual async Task<T> UpdateAsync(string id, T entity)
        {
            return await _collection.FindOneAndReplaceAsync(x => x.Id == id, entity);
        }

        public virtual async Task<T> UpdateAsync(T entity, Expression<Func<T, bool>> predicate)
        {
            return await _collection.FindOneAndReplaceAsync(predicate, entity);
        }

        public virtual async Task<T> DeleteAsync(T entity)
        {
            return await _collection.FindOneAndDeleteAsync(x => x.Id == entity.Id);
        }

        public virtual async Task<T> DeleteAsync(string id)
        {
            return await _collection.FindOneAndDeleteAsync(x => x.Id == id);
        }

        public virtual async Task<T> DeleteAsync(Expression<Func<T, bool>> filter)
        {
            return await _collection.FindOneAndDeleteAsync(filter);
        }
    }