using GuildWars2;
using GuildWars2.Achievements;
using GuildWars2.Achievements.Dailies;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using Polly.Timeout;
using static System.Net.HttpStatusCode;

var host = new HostBuilder().ConfigureServices(
        services =>
        {
            // Gw2Client with the following policies applied
            // * Each request should complete with a success response in less than 30 seconds
            // * Otherwise retry with exponential delay if the error is considered retryable
            // * Give up after 100 seconds
            services.AddHttpClient<Gw2Client>()
                .AddPolicyHandler(
                    Policy.TimeoutAsync<HttpResponseMessage>(
                        TimeSpan.FromSeconds(100),
                        TimeoutStrategy.Optimistic
                    )
                )
                .AddPolicyHandler(
                    Policy<HttpResponseMessage>
                        .HandleResult(
                            response => response.StatusCode is ServiceUnavailable
                                or GatewayTimeout
                                or BadGateway
                                or TooManyRequests
                        )
                        .Or<TimeoutRejectedException>()
                        .WaitAndRetryForeverAsync(
                            retryAttempt =>
                                TimeSpan.FromSeconds(Math.Min(8, Math.Pow(2, retryAttempt)))
                        )
                )
                .AddPolicyHandler(
                    Policy.TimeoutAsync<HttpResponseMessage>(
                        TimeSpan.FromSeconds(30),
                        TimeoutStrategy.Optimistic
                    )
                );
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
