using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosTomates.PetHolidays.Infrastructure.Services.Rabbit
{
    public class RabbitMqSettings
    {
        public static string SectionName = "RabbitMqSettings";
        public string Host { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ClientQueue { get; set; } = null!;
        public string AgentId { get; set; } = Guid.NewGuid().ToString("N");
        public string WorkQueue { get; set; } = null!;
    }
}
