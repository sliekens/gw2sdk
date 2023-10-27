using System.Text.Json;
using Polly;
using Polly.Hedging;
using Polly.Retry;
using static System.Net.HttpStatusCode;

#if NETFRAMEWORK
using static GuildWars2.Http.HttpStatusCodeEx;
#endif

namespace GuildWars2.Tests.TestInfrastructure
{
    public class ResilienceHandler : DelegatingHandler
    {
        private readonly ResiliencePipeline<HttpResponseMessage> resiliencePipeline;

        public ResilienceHandler(HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
            resiliencePipeline = new ResiliencePipelineBuilder<HttpResponseMessage>().AddRetry(
                    new RetryStrategyOptions<HttpResponseMessage>
                    {
                        ShouldHandle = async attempt => attempt.Outcome switch
                        {
                            // Retry on too many requests
                            { Result.StatusCode: TooManyRequests } => true,

                            // Retry on Service Unavailable just once
                            // because we don't know if it's intentional or due to technical difficulties
                            { Result.StatusCode: ServiceUnavailable } => attempt.AttemptNumber == 0
                                && await GetText(attempt.Outcome) != "API not active",

                            _ => false
                        },
                        MaxRetryAttempts = 100,
                        BackoffType = DelayBackoffType.Constant,
                        Delay = TimeSpan.FromSeconds(30),
                        UseJitter = true
                    }
                )
                .AddHedging(
                    new HedgingStrategyOptions<HttpResponseMessage>
                    {
                        // If no response is received within 30 seconds, abort the in-flight request and retry
                        Delay = TimeSpan.FromSeconds(30),
                        MaxHedgedAttempts = 10,
                        ShouldHandle = async attempt =>
                        {
                            return attempt.Outcome switch
                            {
                                { Result.IsSuccessStatusCode: true } => false,

                                // The following replies are considered retryable without a back-off delay
                                {
                                    Result.StatusCode: InternalServerError
                                    or BadGateway
                                    or GatewayTimeout
                                } => true,

                                // Sometimes the API returns weird data, also treat as internal errors
                                _ => await GetText(attempt.Outcome) is
                                    "endpoint requires authentication"
                                    or "unknown error"
                                    or "ErrBadData"
                                    or "ErrTimeout"
                            };
                        },
                    }
                )
                .Build();

            static async Task<string> GetText(Outcome<HttpResponseMessage> attempt)
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
                    if (!json.RootElement.TryGetProperty("text", out var text))
                    {
                        return null;
                    }

                    // Sometimes you get an authentication error even though your API key is valid
                    // Treat this message as an internal error, because you get a different message if the API key is really invalid
                    return text.GetString();
                }
                finally
                {
                    content.Position = 0;
                }
            }
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken
        )
        {
            return await resiliencePipeline.ExecuteAsync(
                    async ct =>
                    {
                        return await base.SendAsync(request, ct).ConfigureAwait(false);
                    },
                    cancellationToken
                )
                .ConfigureAwait(false);
        }
    }
}
