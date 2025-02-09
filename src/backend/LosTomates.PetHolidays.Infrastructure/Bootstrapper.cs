using LosTomates.PetHolidays.Infrastructure.Services.Rabbit;
using LosTomates.PetHolidays.RabbitMQ.Core.Services.Rabbit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosTomates.PetHolidays.Infrastructure
{
    public static class Bootstrapper
    {
        public static IServiceCollection RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .Configure<RabbitMqSettings>(configuration.GetSection(RabbitMqSettings.SectionName))
                .AddHostedService<RabbitMqRpcServer>()
                .AddSingleton<IRpcClient, RabbitMqRpcClient>();

            return services;
        }
    }
}
