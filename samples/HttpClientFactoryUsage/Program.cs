using GuildWars2;
using GuildWars2.Achievements;
using GuildWars2.Achievements.Dailies;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder().ConfigureServices(
        services =>
        {
            services.AddHttpClient();
            services.AddTransient<Gw2Client>();
        }
    )
    .Build();

var gw2 = host.Services.GetRequiredService<Gw2Client>();
DailyAchievementGroup dailies = await gw2.Achievements.GetDailyAchievements();
HashSet<Achievement> dailyFractals = await gw2.Achievements.GetAchievementsByIds(dailies.Fractals.Select(fractal => fractal.Id).ToList());

Console.WriteLine("Daily fractals");
Console.WriteLine("========================================");
foreach (var fractal in dailyFractals)
{
    Console.WriteLine("{0,-40}: {1}", fractal.Name, fractal.Requirement);
}
