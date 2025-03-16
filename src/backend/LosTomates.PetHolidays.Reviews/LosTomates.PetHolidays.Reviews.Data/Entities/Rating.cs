using LosTomates.PetHolidays.Reviews.Data.Base;

namespace LosTomates.PetHolidays.Reviews.Data.Entities;

public sealed class Rating : MongoEntityBase
{
    public required EntityLink Entity { get; set; }

    public int Reviews { get; set; }

    public float Average { get; set; }
}
