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
            var retry = HttpPolicyExtensions.HandleTransientHttpError().Or<TimeoutRejectedException>().WaitAndRetryAsync(3, i => TimeSpan.FromSeconds(i));
            var bulkahead = Policy.BulkheadAsync<HttpResponseMessage>(600);
            var timeout = Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(1));

            _socketsHandler = new SocketsHttpHandler();
            _policyHttpMessageHandler =
                new PolicyHttpMessageHandler(Policy.WrapAsync(retry, bulkahead, timeout))
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
            HttpBasicAccess.UseLatestSchemaVersion();
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
    }
}
