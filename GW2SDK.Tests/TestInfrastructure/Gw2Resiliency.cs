using System.Text.Json;
using Polly;
using Polly.Hedging;
using Polly.Retry;
using static System.Net.HttpStatusCode;

#if NETFRAMEWORK
using static GuildWars2.Http.HttpStatusCodeEx;
#endif

namespace GuildWars2.Tests.TestInfrastructure;

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
        Delay = TimeSpan.FromSeconds(30),
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
