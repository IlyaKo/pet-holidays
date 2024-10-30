using LosTomates.PetHolidays.Application.Hotels;
using LosTomates.PetHolidays.Core.Domain.Hotels;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosTomates.PetHolidays.Application.Mapping
{
    public class HotelMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Hotel, HotelView>();
        }
    }
}
