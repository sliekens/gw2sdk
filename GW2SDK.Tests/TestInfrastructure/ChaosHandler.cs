using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace GuildWars2.Tests.TestInfrastructure;

internal class ChaosHandler : DelegatingHandler
{
    private static readonly Random Random = new();

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken
    )
    {
        if (request.RequestUri!.Host == "api.guildwars2.com")
        {
            // 1 in 3 requests will fail
            if (Random.Next(3) == 1)
            {
                var json = /*lang=json,strict*/ """
                                                {"text":"unknown error"}
                                                """;
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Headers =
                    {
                        Connection = { "keep-alive" },
                        Date = DateTimeOffset.UtcNow,
                        Server = { ProductInfoHeaderValue.Parse("InMemory") }
                    },
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                    {
                        Headers = { ContentLength = json.Length }
                    }
                };
            }
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
