using LosTomates.PetHolidays.Reviews.Data.Base;
using LosTomates.PetHolidays.Reviews.Data.Entities;
using MongoDB.Driver;

namespace LosTomates.PetHolidays.Reviews.Data.Repositories;

public sealed class RatingRepository(IMongoClient client)
    : MongoRepositoryBase<Rating>(client, MongoConstants.RatingCollectionName);
