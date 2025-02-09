using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosTomates.PetHolidays.RabbitMQ.Core.Services.Rabbit
{
    public interface IRpcClient
    {
        Task<string> Call(string message, string queue);
    }
}
