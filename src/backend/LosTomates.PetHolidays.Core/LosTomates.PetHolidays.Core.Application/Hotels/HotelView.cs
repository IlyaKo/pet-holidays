using LosTomates.PetHolidays.Core.Application.Rooms;

namespace LosTomates.PetHolidays.Core.Application.Hotels;

public class HotelView : HotelShortView
{
    public List<RoomView> Rooms { get; set; } = [];
}
