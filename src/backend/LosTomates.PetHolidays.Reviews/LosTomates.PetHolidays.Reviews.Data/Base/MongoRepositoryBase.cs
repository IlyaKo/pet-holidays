using MongoDB.Driver;
using System.Linq.Expressions;

namespace LosTomates.PetHolidays.Reviews.Data.Base;

public abstract class MongoRepositoryBase<T> where T : MongoEntityBase
{
    protected IMongoDatabase Database { get; }

    protected IMongoCollection<T> Collection { get; }

    public MongoRepositoryBase(IMongoClient client, string collectionName)
    {
        Database = client.GetDatabase(MongoConstants.DatabaseName);
        Collection = Database.GetCollection<T>(collectionName);
    }

    public virtual async Task<IEnumerable<T>> GetAll()
    {
        return await Collection.Find(_ => true).ToListAsync();
    }

    public virtual async Task<T> GetById(string id)
    {
        return await Collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public virtual async Task<IEnumerable<T>> GetRangeByIds(List<string> ids)
    {
        return await Collection.Find(x => ids.Contains(x.Id)).ToListAsync();
    }

    public virtual async Task<T> GetFirstWhere(Expression<Func<T, bool>> predicate)
    {
        return await Collection.Find(predicate).FirstOrDefaultAsync();
    }

    public virtual async Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate)
    {
        return await Collection.Find(predicate).ToListAsync();
    }

    public virtual async Task Add(T entity)
    {
        await Collection.InsertOneAsync(entity);
    }

    public virtual async Task Update(T entity)
    {
        await Collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
    }

    public virtual async Task Delete(T entity)
    {
        await Collection.DeleteOneAsync(x => x.Id == entity.Id);
    }

    public virtual async Task Delete(string id)
    {
        await Collection.DeleteOneAsync(x => x.Id == id);
    }
}
