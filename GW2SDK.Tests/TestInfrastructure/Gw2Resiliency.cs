using System.Text.Json;

using Polly;
using Polly.CircuitBreaker;
using Polly.Hedging;
using Polly.Retry;
using Polly.Timeout;

using static System.Net.HttpStatusCode;

#if NETFRAMEWORK
using static GuildWars2.Http.HttpStatusCodeEx;
#endif

namespace GuildWars2.Tests.TestInfrastructure;

// Static class to hold the resiliency strategies
// 
// Usage:
//  resiliencePipelineBuilder.AddTimeout(Gw2Resiliency.TotalTimeoutStrategy)
//      .AddRetry(Gw2Resiliency.RetryStrategy)
//      .AddCircuitBreaker(Gw2Resiliency.CircuitBreakerStrategy)
//      .AddHedging(Gw2Resiliency.HedgingStrategy)
//      .AddTimeout(Gw2Resiliency.AttemptTimeoutStrategy)
public static class Gw2Resiliency
{
    public static readonly TimeoutStrategyOptions TotalTimeoutStrategy = new()
    {
        Timeout = TimeSpan.FromMinutes(5)
    };

    // The API can return errors which can be fixed by a delayed retry
    public static readonly RetryStrategyOptions<HttpResponseMessage> RetryStrategy = new()
    {
        MaxRetryAttempts = 10,
        Delay = TimeSpan.FromSeconds(10),
        BackoffType = DelayBackoffType.Constant,
        UseJitter = true,
        ShouldHandle = static async attempt => attempt.Outcome switch
        {
            { Exception: OperationCanceledException } => !attempt.Context.CancellationToken
                .IsCancellationRequested,
            { Exception: HttpRequestException } => true,
            { Exception: TimeoutRejectedException } => true,
            { Exception: BrokenCircuitException } => true,
            { Result.StatusCode: RequestTimeout } => true,
            { Result.StatusCode: TooManyRequests } => true,
            { Result.StatusCode: InternalServerError } => true,
            { Result.StatusCode: BadGateway } => true,
            { Result.StatusCode: ServiceUnavailable } => await GetText(attempt.Outcome)
                != "API not active",
            { Result.StatusCode: GatewayTimeout } => true,

            // Sometimes the API returns weird data, also treat as internal errors
            { Result.IsSuccessStatusCode: false, Result.Content.Headers.ContentLength: 0 } => true,
            { Result.IsSuccessStatusCode: false } => await GetText(attempt.Outcome) is
                "endpoint requires authentication"
                or "unknown error"
                or "ErrBadData"
                or "ErrTimeout",

            _ => false
        }
    };

    public static readonly CircuitBreakerStrategyOptions<HttpResponseMessage>
        CircuitBreakerStrategy = new()
        {
            ShouldHandle = static async attempt => attempt.Outcome switch
            {
                { Exception: OperationCanceledException } => !attempt.Context.CancellationToken
                    .IsCancellationRequested,
                { Exception: HttpRequestException } => true,
                { Exception: TimeoutRejectedException } => true,
                { Result.StatusCode: RequestTimeout } => true,
                { Result.StatusCode: TooManyRequests } => true,
                { Result.StatusCode: InternalServerError } => true,
                { Result.StatusCode: BadGateway } => true,
                { Result.StatusCode: ServiceUnavailable } => await GetText(attempt.Outcome)
                    != "API not active",
                { Result.StatusCode: GatewayTimeout } => true,

                // Sometimes the API returns weird data, also treat as internal errors
                {
                    Result.IsSuccessStatusCode: false, Result.Content.Headers.ContentLength: 0
                } => true,
                { Result.IsSuccessStatusCode: false } => await GetText(attempt.Outcome) is
                    "endpoint requires authentication"
                    or "unknown error"
                    or "ErrBadData"
                    or "ErrTimeout",

                _ => false
            }
        };

    // The API can be slow or misbehave,
    // Use a hedging strategy to perform immediate retries
    public static readonly HedgingStrategyOptions<HttpResponseMessage> HedgingStrategy = new()
    {
        // If no response is received within 10 seconds, start a second request (without cancelling the first one.)
        // As soon as either request completes successfully, the other one is cancelled.
        Delay = TimeSpan.FromSeconds(10),

        // Additionally, hedge certain errors which might succeed on a second attempt without waiting
        ShouldHandle = static async attempt => attempt.Outcome switch
        {
            { Result.StatusCode: RequestTimeout } => true,
            { Result.StatusCode: InternalServerError } => true,
            { Result.StatusCode: BadGateway } => true,
            { Result.StatusCode: GatewayTimeout } => true,

            // Sometimes the API returns weird data, also treat as internal errors
            { Result.IsSuccessStatusCode: false, Result.Content.Headers.ContentLength: 0 } => true,
            { Result.IsSuccessStatusCode: false } => await GetText(attempt.Outcome) is
                "endpoint requires authentication"
                or "unknown error"
                or "ErrBadData"
                or "ErrTimeout",

            _ => false
        }
    };

    public static readonly TimeoutStrategyOptions AttemptTimeoutStrategy = new()
    {
        Timeout = TimeSpan.FromSeconds(100)
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

        // IMPORTANT: buffer the Content to make ReadAsStreamAsync return a rewindable MemoryStream
        await attempt.Result.Content.LoadIntoBufferAsync();

        // ALSO IMPORTANT: do not dispose the MemoryStream because subsequent ReadAsStreamAsync calls return the same instance
        var content = await attempt.Result.Content.ReadAsStreamAsync();
        try
        {
            using var json = await JsonDocument.ParseAsync(content);
            return json.RootElement.TryGetProperty("text", out var text) ? text.GetString() : null;
        }
        finally
        {
            // ALSO IMPORTANT: rewind the stream for subsequent reads
            content.Position = 0;
        }
    }
}
