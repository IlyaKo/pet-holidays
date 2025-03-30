using LosTomates.PetHolidays.Reviews.Application.Ratings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LosTomates.PetHolidays.Reviews.BackgroundTasks;

public class UpdateRatingsBackgroundService(IServiceProvider serviceProvider) : BackgroundService
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var scope = _serviceProvider.CreateScope();
        var ratingService = scope.ServiceProvider.GetRequiredService<RatingService>();

        while (!stoppingToken.IsCancellationRequested)
        {
            await ratingService.UpdateRatings();
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }
}
