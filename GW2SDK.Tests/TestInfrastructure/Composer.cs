﻿namespace GuildWars2.Tests.TestInfrastructure;

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
            PooledConnectionLifetime = TimeSpan.FromMinutes(5)
        };
#else
        PrimaryHttpHandler = new HttpClientHandler { MaxConnectionsPerServer = 20 };
#endif

        ResilientHttpHandler = new ResilienceHandler(new LoggingHandler
        {
            InnerHandler = new ChaosHandler
            {
                InnerHandler = PrimaryHttpHandler
            }
        });
    }

    public static T Resolve<T>() =>
        (T?)GetService(typeof(T))
        ?? throw new InvalidOperationException($"Unable to compose type '{typeof(T)}'");

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
            return new SchemaVersionHandler
            {
                InnerHandler = ResilientHttpHandler
            };
        }

        static HttpClient HttpClient()
        {
            return new HttpClient(HttpMessageHandler(), disposeHandler: false)
            {
                Timeout = TimeSpan.FromMinutes(5)
            };
        }
    }
}
