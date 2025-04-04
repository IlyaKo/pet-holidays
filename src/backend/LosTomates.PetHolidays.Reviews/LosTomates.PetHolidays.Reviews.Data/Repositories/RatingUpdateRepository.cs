using LosTomates.PetHolidays.Reviews.Data.Base;
using LosTomates.PetHolidays.Reviews.Data.Entities;
using MongoDB.Driver;

namespace LosTomates.PetHolidays.Reviews.Data.Repositories;

public sealed class RatingUpdateRepository(IMongoClient client)
    : MongoRepositoryBase<RatingUpdate>(client, MongoConstants.RatingUpdateCollectionName);
