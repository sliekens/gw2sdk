using System;
using System.Net.Http;
using GW2SDK.Exceptions;
using Polly;
using Polly.Extensions.Http;
using Polly.Timeout;

namespace GW2SDK.Tests.TestInfrastructure
{
    public class HttpPolicy
    {
        public static IAsyncPolicy<HttpResponseMessage> Retry = HttpPolicyExtensions.HandleTransientHttpError().Or<TimeoutRejectedException>().RetryAsync(10);

        public static IAsyncPolicy<HttpResponseMessage> Bulkhead = Policy.BulkheadAsync<HttpResponseMessage>(600);

        public static IAsyncPolicy<HttpResponseMessage> InnerTimeout = Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(10));

        public static IAsyncPolicy<HttpResponseMessage> OuterTimeout = Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(100));

        private static readonly Random Jitterer = new Random();

        public static IAsyncPolicy<HttpResponseMessage> SelectPolicy(HttpRequestMessage request)
        {
            var rateLimit = Policy.Handle<TooManyRequestsException>()
                                  .WaitAndRetryForeverAsync(retryAttempt =>
                                      TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)) + TimeSpan.FromMilliseconds(Jitterer.Next(0, 1000)));

            bool IsRetryable(HttpRequestMessage requestMessage)
            {
                return requestMessage.Method == HttpMethod.Get;
            }

            // We typically need these policies, in order:
            // 1. Outer TimeoutPolicy, to prevent waiting too long for the remaining policies to play out
            // 2. Wait and Retry Policy for rate limit erors
            // 3. CircuitBreaker, to avoid hammering the API when it's having serious issues
            // 4. RetryPolicy (with small jittered wait?), to reduce crashes for retryable errors (timeout etc)
            // 5. Bulkhead, because API is throttled
            // 6. Inner TimeoutPolicy, because API is known to be unresponsive sometimes
            var policy = OuterTimeout.WrapAsync(rateLimit);
            if (IsRetryable(request))
            {
                policy = policy.WrapAsync(Retry);
            }

            return policy.WrapAsync(Bulkhead).WrapAsync(InnerTimeout);
        }
    }
}
