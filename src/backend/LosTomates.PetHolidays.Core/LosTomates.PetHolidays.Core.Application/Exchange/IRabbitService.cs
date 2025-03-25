namespace LosTomates.PetHolidays.Core.Core.Exchange;

public interface IRabbitService
{
    Task SendEntityDeletedEvent(EntityDeletedEventDto dto);
}