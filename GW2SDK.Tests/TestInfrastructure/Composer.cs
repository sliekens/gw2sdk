using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;

using Microsoft.Extensions.Http.Resilience;

using Polly;

namespace GuildWars2.Tests.TestInfrastructure;

public static class Composer
{
    private static readonly HttpMessageHandler PrimaryHttpHandler = CreatePrimaryHttpHandler();

    private static readonly HttpMessageHandler ResilientHttpHandler = CreateResilientHttpHandler();

#if NET
    private static SocketsHttpHandler CreatePrimaryHttpHandler()
    {
        return new SocketsHttpHandler
        {
            // Limit the number of open connections
            //   because we have many tests trying to use the API concurrently,
            //   resulting in a stupid amount of connections being opened
            // The desired effect is to open a smaller number of connections that are reused often
            MaxConnectionsPerServer = 20,

            // Creating a new connection shouldn't take more than 10 seconds
            ConnectTimeout = TimeSpan.FromSeconds(10),
            PooledConnectionLifetime = TimeSpan.FromMinutes(15)
        };
    }
#else
    private static HttpClientHandler CreatePrimaryHttpHandler()
    {
        return new HttpClientHandler { MaxConnectionsPerServer = 20 };
    }
#endif

    private static ResilienceHandler CreateResilientHttpHandler()
    {
        ResiliencePipeline<HttpResponseMessage> resiliencePipeline = new ResiliencePipelineBuilder<HttpResponseMessage>()
            .AddTimeout(Gw2Resiliency.TotalTimeoutStrategy)
            .AddRetry(Gw2Resiliency.RetryStrategy)
            .AddCircuitBreaker(Gw2Resiliency.CircuitBreakerStrategy)
            .AddHedging(Gw2Resiliency.HedgingStrategy)
            .AddTimeout(Gw2Resiliency.AttemptTimeoutStrategy)
            .Build();

        return new ResilienceHandler(resiliencePipeline)
        {
            InnerHandler = new LoggingHandler
            {
                InnerHandler = new ChaosHandler { InnerHandler = PrimaryHttpHandler }
            }
        };
#pragma warning restore EXTEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
    }

    public static T Resolve<T>()
    {
        return (T?)GetService(typeof(T))
            ?? throw new InvalidOperationException($"Unable to compose type '{typeof(T)}'");
    }

    [SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "The allocating method does not have dispose ownership")]
    private static object? GetService(Type serviceType)
    {
        if (serviceType == typeof(Gw2Client))
        {
            return new Gw2Client(HttpClient());
        }

        if (serviceType == typeof(HttpClient))
        {
            return HttpClient();
        }

        if (serviceType == typeof(HttpMessageHandler))
        {
            return HttpMessageHandler();
        }

        return null;

        static HttpMessageHandler HttpMessageHandler()
        {
            return new SchemaVersionHandler { InnerHandler = ResilientHttpHandler };
        }

        static HttpClient HttpClient()
        {
            return new HttpClient(HttpMessageHandler(), false)
            {
                Timeout = TimeSpan.FromMinutes(5),
                DefaultRequestHeaders =
                {
                    UserAgent =
                    {
                        ProductInfoHeaderValue.Parse(
                            $"{typeof(HttpClient).FullName}/{Environment.Version}"
                        )
                    }
                }
            };
        }
    }
}
