using LosTomates.PetHolidays.Core.Domain.Users;

namespace LosTomates.PetHolidays.Application.Users;

public sealed class UserView
{
    public int Id { get; set; }
    public  string Name { get; set; }
    public string? Email { get; set; }
    public  string Phone { get; set; }
    public  string Password { get; set; }
    public DateTime? CreatedDate { get; set; }
    public UserView(User source)
    {
        Id = source.Id;
        Name = source.Name;
        Email = source.Email;
        Phone = source.Phone;
        Password = source.Password;
        CreatedDate = source.CreatedDate;
    }
}
