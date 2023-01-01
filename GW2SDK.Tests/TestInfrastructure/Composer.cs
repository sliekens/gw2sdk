using System;
using System.Net.Http;

namespace GuildWars2.Tests.TestInfrastructure;

public static class Composer
{
    private static readonly ConfigurationManager Configuration = new();

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
        if (serviceType == typeof(TestCharacterName))
        {
            return new TestCharacterName(Configuration.CharacterName);
        }

        if (serviceType == typeof(ApiKeyBasic))
        {
            return new ApiKeyBasic(Configuration.ApiKeyBasic);
        }

        if (serviceType == typeof(ApiKey))
        {
            return new ApiKey(Configuration.ApiKey);
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
