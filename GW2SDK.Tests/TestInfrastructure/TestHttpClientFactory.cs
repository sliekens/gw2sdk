using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Http;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Timeout;

namespace GW2SDK.Tests.TestInfrastructure
{
    public class TestHttpClientFactory : IHttpClientFactory, IAsyncDisposable
    {
        private readonly ServiceProvider _httpClientProvider = BuildHttpClientProvider();

        public async ValueTask DisposeAsync()
        {
            await _httpClientProvider.DisposeAsync()
                .ConfigureAwait(false);
            GC.SuppressFinalize(this);
        }

        public HttpClient CreateClient(string name) =>
            _httpClientProvider.GetRequiredService<IHttpClientFactory>()
                .CreateClient(name);

        /// <summary>Creates a service provider for the HTTP factory which is unfortunately very dependent on ServiceCollection.</summary>
        private static ServiceProvider BuildHttpClientProvider()
        {
            var services = new ServiceCollection();

            AddPolicies(services);

            services.AddTransient<UnauthorizedMessageHandler>();
            services.AddTransient<BadMessageHandler>();
            services.AddTransient<RateLimitHandler>();
            services.AddHttpClient("GW2SDK",
                    http =>
                    {
                        http.BaseAddress = ConfigurationManager.Instance.BaseAddress;
                        http.UseSchemaVersion(SchemaVersion.Latest);
                    })
                .ConfigurePrimaryHttpMessageHandler(() =>
                {
#if NET
                    return new SocketsHttpHandler
                    {
                        MaxConnectionsPerServer = 10,
                        AutomaticDecompression = DecompressionMethods.GZip
                    };
#else
                    return new HttpClientHandler
                    {
                        MaxConnectionsPerServer = 10,
                        AutomaticDecompression = DecompressionMethods.GZip
                    };
#endif
                })
                .AddPolicyHandlerFromRegistry("api.guildwars2.com")
                .AddHttpMessageHandler<UnauthorizedMessageHandler>()
                .AddHttpMessageHandler<BadMessageHandler>()
                .AddHttpMessageHandler<RateLimitHandler>();

            return services.BuildServiceProvider();
        }

        private static void AddPolicies(IServiceCollection services)
        {
            var jitterer = new Random();
            var policies = services.AddPolicyRegistry();

            // Any individual request should be able to complete in 30 seconds or less
            var innerTimeout =
                Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(30), TimeoutStrategy.Optimistic);

            // Unfortunately the API limits the number of requests over time from the same IP address.
            // This is annoying because we can't do anything to stay within the limit.
            // Other processes on the same machine (or on another machine on the same network) could also be using the API and contributing to the limit.
            // Basically, we might already be at the limit on a cold startup.
            // A circuit breaker wouldn't help in this case. (Only when you can guarantee that there are no other API users using the same IP address.)
            // The only thing that works well in all environments is automatic retries with exponential and jittered sleep durations.
            var rateLimit = Policy<HttpResponseMessage>.Handle<TooManyRequestsException>()
                .WaitAndRetryForeverAsync(retryAttempt => TimeSpan.FromSeconds(Math.Min(8, Math.Pow(2, retryAttempt))) +
                    TimeSpan.FromMilliseconds(jitterer.Next(0, 1000)));

            // Give up when there is no usable response within a reasonable time
            //
            // Let's look at a very pessimistic scenario that still completes successfully to determine what is reasonable
            // * The rate limit is reached
            // * The latency is high (possibly because the network is saturated)
            // * The API returns a success response on the third retry (four attempts total)
            //
            //              Duration    Total duration
            // Request         500ms             500ms
            // X-Rate error    500ms           1,000ms
            // Sleep         3,000ms           4,000ms
            // Request         500ms           4,500ms
            // X-Rate error    500ms           5,000ms
            // Sleep         5,000ms          10,000ms
            // Request         500ms          10,500ms
            // X-Rate error    500ms          11,000ms
            // Sleep         9,000ms          20,000ms
            // Request         500ms          20,500ms
            // OK result    29,999ms          50,499ms
            //
            // This is already very pessimistic but let's round it up to a minute just to be sure we're not being unreasonable
            var timeout =
                Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(60), TimeoutStrategy.Optimistic);

            policies.Add("api.guildwars2.com", Policy.WrapAsync(timeout, rateLimit, innerTimeout));
        }
    }
}
