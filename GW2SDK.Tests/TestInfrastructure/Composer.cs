using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GW2SDK.Tests.TestInfrastructure;

public class Composer : IServiceProvider, IAsyncDisposable
{
    private readonly ConfigurationManager configuration = new();

    private readonly CompositeDisposable disposables = new();

    private readonly IHttpClientFactory httpClientFactory;

    public Composer()
    {
        var gw2HttpClientFactory = new TestHttpClientFactory(BaseAddress.DefaultUri);
        disposables.Add(gw2HttpClientFactory);
        httpClientFactory = gw2HttpClientFactory;
        SchemaVersion.Default = SchemaVersion.Latest;
    }

    public async ValueTask DisposeAsync()
    {
        await disposables.DisposeAsync().ConfigureAwait(false);
        GC.SuppressFinalize(this);
    }

    public object GetService(Type serviceType)
    {
        if (serviceType == typeof(TestCharacterName))
        {
            return new TestCharacterName(configuration.CharacterName);
        }

        if (serviceType == typeof(ApiKeyBasic))
        {
            return new ApiKeyBasic(configuration.ApiKeyBasic);
        }

        if (serviceType == typeof(ApiKey))
        {
            return new ApiKey(configuration.ApiKey);
        }

        if (serviceType == typeof(HttpClient))
        {
            return httpClientFactory.CreateClient("GW2SDK");
        }

        if (serviceType == typeof(Gw2Client))
        {
            return new Gw2Client(Resolve<HttpClient>());
        }

        return null;
    }

    public T Resolve<T>() =>
        (T)GetService(typeof(T))
        ?? throw new InvalidOperationException($"Unable to compose type '{typeof(T)}'");
}
