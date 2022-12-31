using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using GuildWars2.Http;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Timeout;
using static System.Net.HttpStatusCode;

namespace GuildWars2.Tests.TestInfrastructure;

public class TestHttpClientFactory : IHttpClientFactory, IAsyncDisposable
{
    private readonly ServiceProvider httpClientProvider;

    public TestHttpClientFactory(Uri baseAddress)
    {
        httpClientProvider = BuildHttpClientProvider(baseAddress);
    }

    public async ValueTask DisposeAsync()
    {
        await httpClientProvider.DisposeAsync().ConfigureAwait(false);
        GC.SuppressFinalize(this);
    }

    public HttpClient CreateClient(string name) =>
        httpClientProvider.GetRequiredService<IHttpClientFactory>().CreateClient(name);

    /// <summary>Creates a service provider for the HTTP factory which is unfortunately very dependent on ServiceCollection.</summary>
    private static ServiceProvider BuildHttpClientProvider(Uri baseAddress)
    {
        var services = new ServiceCollection();

        services.AddTransient<SchemaVersionHandler>();

        services.AddHttpClient(
                "GW2SDK",
                http =>
                {
                    http.BaseAddress = baseAddress;
                    http.Timeout = TimeSpan.FromMinutes(1); // default 100 seconds is too long
                }
            )
            .AddHttpMessageHandler<SchemaVersionHandler>()
            .AddPolicyHandler(
                Policy<HttpResponseMessage>.HandleResult(
                        response => response.StatusCode is ServiceUnavailable
                            or GatewayTimeout
                            or BadGateway
                            or (HttpStatusCode)429 // TooManyRequests
                    )
                    .Or<TimeoutRejectedException>()
                    .Or<UnauthorizedOperationException>(

                        // Sometimes the API fails to validate the access key
                        // This is a server error, real token problems result in a different error
                        // eg. "Invalid access token"
                        reason => reason.Message == "endpoint requires authentication"
                    )
                    .WaitAndRetryForeverAsync(
                        retryAttempt => TimeSpan.FromSeconds(Math.Min(8, Math.Pow(2, retryAttempt)))
                    )
            )
            .AddPolicyHandler(
                Policy.TimeoutAsync<HttpResponseMessage>(
                    TimeSpan.FromSeconds(30),
                    TimeoutStrategy.Optimistic
                )
            );

        return services.BuildServiceProvider();
    }
}
