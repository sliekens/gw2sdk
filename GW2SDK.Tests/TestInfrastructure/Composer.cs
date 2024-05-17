namespace GuildWars2.Tests.TestInfrastructure;

public static class Composer
{
    public static T Resolve<T>() =>
        (T)GetService(typeof(T))
        ?? throw new InvalidOperationException($"Unable to compose type '{typeof(T)}'");

    private static object GetService(Type serviceType)
    {
        if (serviceType == typeof(Gw2Client))
        {
            return new Gw2Client(Resolve<HttpClient>());
        }

        if (serviceType == typeof(HttpClient))
        {
            return new HttpClient(
                new SchemaVersionHandler
                {
                    InnerHandler = new ResilienceHandler(
#if NET
                        new SocketsHttpHandler
                        {
                            // Limit the number of open connections
                            //   because we have many tests trying to use the API concurrently,
                            //   resulting in a stupid amount of connections being opened
                            // The desired effect is to open a smaller number of connections that are reused often
                            MaxConnectionsPerServer = 20,

                            // Creating a new connection shouldn't take more than 10 seconds
                            ConnectTimeout = TimeSpan.FromSeconds(10),
                            PooledConnectionLifetime = TimeSpan.FromMinutes(5)
                        }
#else
                        new HttpClientHandler { MaxConnectionsPerServer = 20 }
#endif
                    )
                }
            )
            {
                // The default timeout is 100 seconds, but it's not always enough
                // Requests can get stuck in a delayed retry-loop due to rate limiting
                // A better solution might be to queue up requests
                //   (so that new requests have to wait until there are no more delayed requests)
                Timeout = TimeSpan.FromMinutes(5)
            };
        }

        return null;
    }
}
