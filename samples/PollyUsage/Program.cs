using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using GuildWars2;
using GuildWars2.Commerce.Prices;
using GuildWars2.Items;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Http.Resilience;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Polly;
using Polly.Hedging;
using Polly.Retry;
using static System.Net.HttpStatusCode;

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
    builder =>
    {
        builder.AddRetry(
            new RetryStrategyOptions<HttpResponseMessage>
            {
                ShouldHandle = attempt => attempt.Outcome switch
                {
                    // Retry on too many requests
                    { Result.StatusCode: TooManyRequests } => PredicateResult.True(),

                    // Retry on Service Unavailable just once
                    // because we don't know if it's intentional or due to technical difficulties
                    { Result.StatusCode: ServiceUnavailable } when attempt.AttemptNumber == 0 =>
                        PredicateResult.True(),

                    _ => PredicateResult.False()
                },
                MaxRetryAttempts = 100,
                BackoffType = DelayBackoffType.Constant,
                Delay = TimeSpan.FromSeconds(10),
                UseJitter = true
            }
        );

        // API can be slow or misbehave, use a hedging strategy to retry without delay
        builder.AddHedging(
            new HedgingStrategyOptions<HttpResponseMessage>
            {
                // If no response is received within 30 seconds, abort the in-flight request and retry
                Delay = TimeSpan.FromSeconds(30),
                ShouldHandle = async attempt => attempt.Outcome switch
                {
                    { Result.IsSuccessStatusCode: true } => false,

                    // The following replies are considered retryable without a back-off delay
                    { Result.StatusCode: InternalServerError or BadGateway or GatewayTimeout } =>
                        true,

                    // Sometimes the API returns weird data, also treat as internal errors
                    _ when await IsUnknownError(attempt) => true,

                    _ => false
                }
            }
        );
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
await foreach (ItemPrice itemPrice in gw2.Commerce.GetItemPricesBulk())
{
    // ItemPrice contains an Id, BestBid, and BestAsk
    // Use the ID to get the item name
    Item item = await gw2.Items.GetItemById(itemPrice.Id);

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

async Task<bool> IsUnknownError(HedgingPredicateArguments<HttpResponseMessage> attempt)
{
    if (attempt.Outcome.Result is null)
    {
        return false;
    }

    if (attempt.Outcome.Result.Content.Headers.ContentType?.MediaType != "application/json")
    {
        return true;
    }

    // IMPORTANT: buffer the content so it can be read multiple times if needed
    await attempt.Outcome.Result.Content.LoadIntoBufferAsync();

    // ALSO IMPORTANT: do not dispose the content stream
    var content = await attempt.Outcome.Result.Content.ReadAsStreamAsync();
    try
    {
        using var json = await JsonDocument.ParseAsync(content);
        if (!json.RootElement.TryGetProperty("text", out var text))
        {
            return true;
        }

        // Sometimes you get an authentication error even though your API key is valid
        // Treat this message as an internal error, because you get a different message if the API key is really invalid
        return text.GetString() is "endpoint requires authentication"
            or "unknown error"
            or "ErrBadData"
            or "ErrTimeout";
    }
    finally
    {
        content.Position = 0;
    }
}
