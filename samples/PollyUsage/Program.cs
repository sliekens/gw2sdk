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
            services.AddHttpClient<Gw2Client>(
                    httpClient =>
                    {
                        // Configure a timeout after which OperationCanceledException is thrown
                        // The default timeout is 100 seconds, but it's not always enough for background work
                        //  because requests can get stuck in a delayed retry-loop due to rate limiting
                        httpClient.Timeout = TimeSpan.FromSeconds(600);

                        // For user interactive apps, you want to set a lower timeout
                        // to avoid long waiting periods when there are technical difficulties
                        httpClient.Timeout = TimeSpan.FromSeconds(20);
                    }
                )
                // The API has rate limiting (by IP address) so wait and retry when the server indicates too many requests
                .AddPolicyHandler(Policy.HandleResult<HttpResponseMessage>(response => response.StatusCode == TooManyRequests).WaitAndRetryAsync(10, _ => TimeSpan.FromSeconds(10)))

                // The API can be disabled intentionally to avoid leaking spoilers, or it can be unavailable due to technical difficulties
                // Since it's not easy to tell the difference, give it one retry
                .AddPolicyHandler(Policy.HandleResult<HttpResponseMessage>(response => response.StatusCode == ServiceUnavailable).RetryAsync())

                // Assume internal errors are retryable (within reason)
                .AddPolicyHandler(Policy.HandleResult<HttpResponseMessage>(response => response.StatusCode is InternalServerError or BadGateway or GatewayTimeout).RetryAsync(5))

                // Sometimes the API returns a Bad Request with an unknown error for perfectly valid requests
                .AddPolicyHandler(Policy<HttpResponseMessage>.Handle<ArgumentException>(reason => reason.Message == "unknown error").RetryAsync())

                // Abort each attempted request after max 20 seconds and perform retries (within reason)
                .AddPolicyHandler(Policy<HttpResponseMessage>.Handle<TimeoutRejectedException>().RetryAsync(10))
                .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(20), TimeoutStrategy.Optimistic));
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
