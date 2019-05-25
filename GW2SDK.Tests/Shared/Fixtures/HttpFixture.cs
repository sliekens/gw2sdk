using System;
using System.Net.Http;
using GW2SDK.Extensions;
using Microsoft.Extensions.Http;
using Polly;
using Polly.Extensions.Http;
using Polly.Timeout;

namespace GW2SDK.Tests.Shared.Fixtures
{
    public class HttpFixture : IDisposable
    {
        private readonly ConfigurationFixture _configuration = new ConfigurationFixture();

        private readonly HttpMessageHandler _policyHttpMessageHandler;

        private readonly HttpMessageHandler _socketsHandler;

        public HttpFixture()
        {
            var retry = HttpPolicyExtensions.HandleTransientHttpError().Or<TimeoutRejectedException>()
                .WaitAndRetryAsync(3, i => TimeSpan.FromSeconds(i));
            var bulkhead = Policy.BulkheadAsync<HttpResponseMessage>(600);
            var innerTimeout = Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(10));
            var outerTimeout = Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(60));

            _socketsHandler = new SocketsHttpHandler();
            _policyHttpMessageHandler =
                new PolicyHttpMessageHandler(request =>
                {
                    // We typically need these policies, in order:
                    // 1. Outer TimeoutPolicy, to prevent waiting too long for the remaining policies to play out
                    // 2. CircuitBreaker, to avoid hammering the API when it's having serious issues
                    // 3. RetryPolicy (with small jittered wait?), to reduce crashes for retryable errors (timeout etc)
                    // 4. Bulkhead, because API is throttled
                    // 5. Inner TimeoutPolicy, because API is known to be unresponsive sometimes
                    if (IsRetryable(request))
                    {
                            return Policy.WrapAsync(outerTimeout, retry, bulkhead, innerTimeout);
                    }
                    else
                    {
                        return Policy.WrapAsync(outerTimeout, bulkhead, innerTimeout);
                    }
                })
                {
                    InnerHandler = _socketsHandler
                };

            Http = new HttpClient(_policyHttpMessageHandler);
            Http.UseBaseAddress(_configuration.BaseAddress);
            Http.UseLatestSchemaVersion();

            HttpBasicAccess = new HttpClient(_policyHttpMessageHandler);
            HttpBasicAccess.UseBaseAddress(_configuration.BaseAddress);
            HttpBasicAccess.UseAccessToken(_configuration.ApiKeyBasic);
            HttpBasicAccess.UseLatestSchemaVersion();

            HttpFullAccess = new HttpClient(_policyHttpMessageHandler);
            HttpFullAccess.UseBaseAddress(_configuration.BaseAddress);
            HttpFullAccess.UseAccessToken(_configuration.ApiKeyFull);
            HttpFullAccess.UseLatestSchemaVersion();
        }

        public HttpClient Http { get; }

        public HttpClient HttpBasicAccess { get; }

        public HttpClient HttpFullAccess { get; }

        public void Dispose()
        {
            _socketsHandler?.Dispose();
            _policyHttpMessageHandler?.Dispose();
            Http?.Dispose();
            HttpBasicAccess?.Dispose();
            HttpFullAccess?.Dispose();
        }

        private bool IsRetryable(HttpRequestMessage request) => request.Method == HttpMethod.Get;
    }
}
