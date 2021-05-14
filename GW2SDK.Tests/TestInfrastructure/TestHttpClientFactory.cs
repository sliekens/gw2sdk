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
            await _httpClientProvider.DisposeAsync().ConfigureAwait(false);
            GC.SuppressFinalize(this);
        }

        public HttpClient CreateClient(string name) =>
            _httpClientProvider.GetRequiredService<IHttpClientFactory>().CreateClient(name);

        /// <summary>Creates a service provider for the HTTP factory which is unfortunately very dependent on ServiceCollection.</summary>
        private static ServiceProvider BuildHttpClientProvider()
        {
            var jitterer = new Random();
            var services = new ServiceCollection();
            var policies = services.AddPolicyRegistry();
            var innerTimeout =
                Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(10), TimeoutStrategy.Optimistic);
            var immediateRetry = Policy.Handle<HttpRequestException>()
                .Or<TimeoutException>()
                .Or<TimeoutRejectedException>()
                .OrResult<HttpResponseMessage>(r => (int) r.StatusCode >= 500)
                .RetryForeverAsync();

            var rateLimit = Policy.Handle<TooManyRequestsException>()
                .WaitAndRetryForeverAsync(retryAttempt =>
                    TimeSpan.FromSeconds(Math.Min(8, Math.Pow(2, retryAttempt))) +
                    TimeSpan.FromMilliseconds(jitterer.Next(0, 1000)));
            policies.Add("Http", rateLimit.WrapAsync(innerTimeout));
            policies.Add("HttpIdempotent",
                rateLimit.WrapAsync(immediateRetry).WrapAsync(innerTimeout));
            services.AddTransient<UnauthorizedMessageHandler>();
            services.AddTransient<BadMessageHandler>();
            services.AddTransient<RateLimitHandler>();
            services.AddHttpClient("GW2SDK",
                    http =>
                    {
                        http.BaseAddress = ConfigurationManager.Instance.BaseAddress;
                        http.UseLatestSchemaVersion();
                        http.UseDataCompression();
                    })
                .ConfigurePrimaryHttpMessageHandler(() => new SocketsHttpHandler
                {
                    MaxConnectionsPerServer = 10,
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
                })
                .AddPolicyHandlerFromRegistry((registry, message) =>
                    message.Method == HttpMethod.Post || message.Method == HttpMethod.Patch
                        ? registry.Get<IAsyncPolicy<HttpResponseMessage>>("Http")
                        : registry.Get<IAsyncPolicy<HttpResponseMessage>>("HttpIdempotent"))
                .AddHttpMessageHandler<UnauthorizedMessageHandler>()
                .AddHttpMessageHandler<BadMessageHandler>()
                .AddHttpMessageHandler<RateLimitHandler>();

            return services.BuildServiceProvider();
        }
    }
}
