using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosTomates.PetHolidays.Application.Users
{
    public sealed record UserEditDto
    {
        public required string Name { get; set; }
        public string? Email { get; set; }
        public required string Phone { get; set; }
        public required string Password { get; set; }
    }
}
