using System.Net;
using System.Text;

namespace GuildWars2.Tests.TestInfrastructure;

internal class ChaosHandler : DelegatingHandler
{
    private static readonly Random Random = new();

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (request.RequestUri!.Host == "api.guildwars2.com")
        {
            // 1 in 3 requests will fail
            if (Random.Next(3) == 1)
            {
                var json = /*lang=json,strict*/ """
                        {"text": "unknown error"}
                    """;
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Headers = { { "Server", "InMemory" } },
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                };
            }
        }
        return await base.SendAsync(request, cancellationToken);
    }
}
