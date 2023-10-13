namespace GuildWars2.Tests.TestInfrastructure;

public static class Composer
{
    private static readonly IHttpClientFactory HttpClientFactory;

    static Composer()
    {
        HttpClientFactory = new TestHttpClientFactory(BaseAddress.DefaultUri);
    }

    public static T Resolve<T>() =>
        (T)GetService(typeof(T))
        ?? throw new InvalidOperationException($"Unable to compose type '{typeof(T)}'");

    private static object GetService(Type serviceType)
    {
        if (serviceType == typeof(TestCharacter))
        {
            return TestConfiguration.TestCharacter;
        }

        if (serviceType == typeof(TestGuild))
        {
            return TestConfiguration.TestGuild;
        }

        if (serviceType == typeof(ApiKeyBasic))
        {
            return TestConfiguration.ApiKeyBasic;
        }

        if (serviceType == typeof(ApiKey))
        {
            return TestConfiguration.ApiKey;
        }

        if (serviceType == typeof(Gw2Client))
        {
            var httpClient = HttpClientFactory.CreateClient("GW2SDK");
            return new Gw2Client(httpClient);
        }

        if (serviceType == typeof(HttpClient))
        {
            return HttpClientFactory.CreateClient("GW2SDK");
        }

        return null;
    }
}
