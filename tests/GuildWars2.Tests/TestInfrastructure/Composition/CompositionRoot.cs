using System.Net.Http.Headers;

using Microsoft.Extensions.Http.Resilience;

using Polly;

namespace GuildWars2.Tests.TestInfrastructure.Composition;

#pragma warning disable CA2000 // Dispose objects before losing scope

public sealed class CompositionRoot : IDisposable
{
    private readonly HttpMessageHandler primaryHttpHandler;
    private readonly HttpMessageHandler resilientHttpHandler;

    public CompositionRoot()
    {
        primaryHttpHandler = CreatePrimaryHttpHandler();
        resilientHttpHandler = CreateResilientHttpHandler(primaryHttpHandler);
    }

    public object GetService(Type serviceType)
    {
        ArgumentNullException.ThrowIfNull(serviceType);

        if (serviceType == typeof(Gw2Client))
        {
            SchemaVersionHandler schemaVersionHandler = new()
            {
                InnerHandler = resilientHttpHandler
            };
            HttpClient httpClient = new(schemaVersionHandler)
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

            return new Gw2Client(httpClient);
        }

        throw new InvalidOperationException($"Service of type {serviceType.FullName} is not registered.");
    }

    public Composition CreateScope()
    {
        return new(this);
    }

    public void Dispose()
    {
        resilientHttpHandler.Dispose();
        primaryHttpHandler.Dispose();
    }

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

    private static ResilienceHandler CreateResilientHttpHandler(HttpMessageHandler primaryHttpHandler)
    {
        ResiliencePipeline<HttpResponseMessage> resiliencePipeline =
            new ResiliencePipelineBuilder<HttpResponseMessage>()
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
                InnerHandler = new ChaosHandler { InnerHandler = primaryHttpHandler }
            }
        };
    }
}
