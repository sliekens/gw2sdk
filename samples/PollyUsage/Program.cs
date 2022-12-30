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
            // * Cancel after 1 minute of waiting for a success response (including any retries)
            // * In case of transient errors, retry with exponential delay (2s, 4s, 8s, 8s, 8s ...)
            // * Each individual request (attempt) should complete in less than 10 seconds
            services.AddHttpClient<Gw2Client>(
                    httpClient =>
                    {
                        httpClient.Timeout = TimeSpan.FromMinutes(1);
                    }
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
                        TimeSpan.FromSeconds(10),
                        TimeoutStrategy.Optimistic
                    )
                );
        }
    )
    .Build();

var gw2 = host.Services.GetRequiredService<Gw2Client>();
DailyAchievementGroup dailies = await gw2.Achievements.GetDailyAchievements();
HashSet<Achievement> dailyFractals =
    await gw2.Achievements.GetAchievementsByIds(
        dailies.Fractals.Select(fractal => fractal.Id).ToList()
    );

Console.WriteLine("Daily fractals");
Console.WriteLine("========================================");
foreach (var fractal in dailyFractals)
{
    Console.WriteLine("{0,-40}: {1}", fractal.Name, fractal.Requirement);
}
