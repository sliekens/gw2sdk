using System.Net.Http.Headers;
using Microsoft.Extensions.Http.Resilience;
using Polly;

namespace GuildWars2.Tests.TestInfrastructure;

public static class Composer
{
    private static readonly HttpMessageHandler PrimaryHttpHandler;

    private static readonly HttpMessageHandler ResilientHttpHandler;

    static Composer()
    {
#if NET
        PrimaryHttpHandler = new SocketsHttpHandler
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
#else
        PrimaryHttpHandler = new HttpClientHandler { MaxConnectionsPerServer = 20 };
#endif

        var resiliencePipeline = new ResiliencePipelineBuilder<HttpResponseMessage>()
            .AddTimeout(Gw2Resiliency.TotalTimeoutStrategy)
            .AddRetry(Gw2Resiliency.RetryStrategy)
            .AddCircuitBreaker(Gw2Resiliency.CircuitBreakerStrategy)
            .AddHedging(Gw2Resiliency.HedgingStrategy)
            .AddTimeout(Gw2Resiliency.AttemptTimeoutStrategy)
            .Build();

#pragma warning disable EXTEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
        ResilientHttpHandler = new ResilienceHandler(resiliencePipeline)
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
