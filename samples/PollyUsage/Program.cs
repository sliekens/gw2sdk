using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using GuildWars2;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Polly;
using Polly.Hedging;
using Polly.Retry;
using static System.Net.HttpStatusCode;

namespace PollyUsage;

// Static class to hold the resiliency strategies
// 
// Usage:
//  resiliencePipelineBuilder.AddRetry(Gw2Resiliency.RetryStrategy);
//  resiliencePipelineBuilder.AddHedging(Gw2Resiliency.HedgingStrategy);
public static class Gw2Resiliency
{
    // The API can return errors which can be fixed by a delayed retry
    public static readonly RetryStrategyOptions<HttpResponseMessage> RetryStrategy = new()
    {
        MaxRetryAttempts = 100,
        Delay = TimeSpan.FromSeconds(10),
        BackoffType = DelayBackoffType.Constant,
        UseJitter = true,
        ShouldHandle = async attempt => attempt.Outcome switch
        {
            // Retry on too many requests
            { Result.StatusCode: TooManyRequests } => true,

            // Retry on Service Unavailable just once
            // because we don't know if it's intentional or due to technical difficulties
            { Result.StatusCode: ServiceUnavailable } => attempt.AttemptNumber == 0
                && await GetText(attempt.Outcome) != "API not active",

            _ => false
        }
    };

    // The API can be slow or misbehave,
    // Use a hedging strategy to perform immediate retries
    public static readonly HedgingStrategyOptions<HttpResponseMessage> HedgingStrategy = new()
    {
        // If no response is received within 10 seconds, start a second request (without cancelling the first one.)
        // As soon as either request completes, the other one is cancelled.
        Delay = TimeSpan.FromSeconds(10),
        MaxHedgedAttempts = 10,
        ShouldHandle = async attempt => attempt.Outcome switch
        {
            { Result.IsSuccessStatusCode: true } => false,

            // The following statuses are considered retryable without a back-off delay
            { Result.StatusCode: InternalServerError or BadGateway or GatewayTimeout } => true,

            // Sometimes the API returns weird data, also treat as internal errors
            _ => await GetText(attempt.Outcome) is "endpoint requires authentication"
                or "unknown error"
                or "ErrBadData"
                or "ErrTimeout"
        }
    };

    // Helper method to get the "text" property from the API response
    // because the status code is not always enough to determine the error
    private static async Task<string?> GetText(Outcome<HttpResponseMessage> attempt)
    {
        if (attempt.Result is null)
        {
            return null;
        }

        if (attempt.Result.Content.Headers.ContentType?.MediaType != "application/json")
        {
            return null;
        }

        // IMPORTANT: buffer the content so it can be read multiple times if needed
        await attempt.Result.Content.LoadIntoBufferAsync();

        // ALSO IMPORTANT: do not dispose the content stream
        var content = await attempt.Result.Content.ReadAsStreamAsync();
        try
        {
            using var json = await JsonDocument.ParseAsync(content);
            return json.RootElement.TryGetProperty("text", out var text) ? text.GetString() : null;
        }
        finally
        {
            content.Position = 0;
        }
    }
}

internal class Program
{
    public static async Task Main(string[] args)
    {
        var appBuilder = Host.CreateApplicationBuilder(args);

        var httpClientBuilder = appBuilder.Services.AddHttpClient<Gw2Client>(
            static httpClient =>
            {
                // Configure a timeout after which OperationCanceledException is thrown
                // The default timeout is 100 seconds, but it's not always enough for background work
                //  because requests can get stuck in a delayed retry-loop due to rate limiting
                httpClient.Timeout = TimeSpan.FromSeconds(600);

                // For user interactive apps, you want to set a lower timeout
                // to avoid long waiting periods when there are technical difficulties
                httpClient.Timeout = TimeSpan.FromSeconds(20);
            }
        );

        httpClientBuilder.AddResilienceHandler(
            "api.guildwars2.com",
            resiliencePipelineBuilder =>
            {
                resiliencePipelineBuilder.AddRetry(Gw2Resiliency.RetryStrategy);
                resiliencePipelineBuilder.AddHedging(Gw2Resiliency.HedgingStrategy);
            }
        );

        // Log only the failing requests to the console / terminal
        // In Visual Studio, you can view all requests in the Output window
        appBuilder.Logging.AddFilter<ConsoleLoggerProvider>("Polly", LogLevel.Warning);
        appBuilder.Logging.AddFilter<ConsoleLoggerProvider>(
            "System.Net.Http.HttpClient.Gw2Client",
            LogLevel.Warning
        );

        var app = appBuilder.Build();

        var gw2 = app.Services.GetRequiredService<Gw2Client>();

        PrintHeader();

        // Get the trading post prices for all items in bulk
        await foreach (var (itemPrice, _) in gw2.Commerce.GetItemPricesBulk())
        {
            // ItemPrice contains an Id, BestBid, and BestAsk
            // Use the ID to get the item name
            var (item, _) = await gw2.Items.GetItemById(itemPrice.Id);

            PrintRow(item.Name, itemPrice.BestBid, itemPrice.BestAsk);
        }

        void PrintHeader()
        {
            Console.WriteLine(new string('=', 160));
            Console.WriteLine($"| {"Item",-50} | {"Highest buyer",-50} | {"Lowest seller",-50} |");
            Console.WriteLine(new string('=', 160));
        }

        void PrintRow(string item, Coin highestBuyer, Coin lowestSeller)
        {
            Console.WriteLine($"| {item,-50} | {highestBuyer,-50} | {lowestSeller,-50} |");
        }
    }
}
