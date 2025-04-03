using LosTomates.PetHolidays.Reviews.Data.Base;

namespace LosTomates.PetHolidays.Reviews.Data.Entities;

public sealed class RatingUpdate : MongoEntityBase
{
    public required EntityLink Entity { get; set; }

    public DateTime Date { get; set; }
}
