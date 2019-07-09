using System;
using System.Net.Http;
using GW2SDK.Exceptions;
using Polly;
using Polly.Extensions.Http;
using Polly.Timeout;

namespace GW2SDK.TestDataHelper
{
    public class HttpPolicy
    {
        public static IAsyncPolicy<HttpResponseMessage> Retry = HttpPolicyExtensions.HandleTransientHttpError().Or<TooManyRequestsException>().Or<TimeoutRejectedException>().RetryAsync(10);

        public static IAsyncPolicy<HttpResponseMessage> Bulkhead = Policy.BulkheadAsync<HttpResponseMessage>(600);

        public static IAsyncPolicy<HttpResponseMessage> InnerTimeout = Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(10));

        public static IAsyncPolicy<HttpResponseMessage> OuterTimeout = Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(100));

        public static IAsyncPolicy<HttpResponseMessage> SelectPolicy(HttpRequestMessage request)
        {
            bool IsRetryable(HttpRequestMessage requestMessage)
            {
                return requestMessage.Method == HttpMethod.Get;
            }

            // We typically need these policies, in order:
            // 1. Outer TimeoutPolicy, to prevent waiting too long for the remaining policies to play out
            // 2. CircuitBreaker, to avoid hammering the API when it's having serious issues
            // 3. RetryPolicy (with small jittered wait?), to reduce crashes for retryable errors (timeout etc)
            // 4. Bulkhead, because API is throttled
            // 5. Inner TimeoutPolicy, because API is known to be unresponsive sometimes
            if (IsRetryable(request))
            {
                return Policy.WrapAsync(OuterTimeout, Retry, Bulkhead, InnerTimeout);
            }

            return Policy.WrapAsync(OuterTimeout, Bulkhead, InnerTimeout);
        }
    }
}
