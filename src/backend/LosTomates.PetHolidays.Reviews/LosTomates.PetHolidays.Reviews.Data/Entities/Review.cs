using LosTomates.PetHolidays.Reviews.Data.Base;

namespace LosTomates.PetHolidays.Reviews.Data.Entities;

public sealed class Review : MongoEntityBase
{
    public required EntityLink Entity { get; set; }

    public required User User { get; set; }

    public DateTime Date { get; set; }

    public int Stars { get; set; }

    public string? Comment { get; set; }
}
