using GW2SDK;
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

var gw2Client = host.Services.GetRequiredService<Gw2Client>();
var dailies = await gw2Client.Achievements.GetDailyAchievements();
var fractals = await gw2Client.Achievements.GetAchievementsByIds(
    dailies.Value.Fractals.Select(fractal => fractal.Id).ToList()
);

Console.WriteLine("Daily fractals: {0:m}", dailies.Date);
Console.WriteLine("========================================");
foreach (var fractal in fractals)
{
    Console.WriteLine("{0,-40}: {1}", fractal.Name, fractal.Requirement);
}
