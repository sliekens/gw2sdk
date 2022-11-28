using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace GuildWars2.Http;

internal static class Gw2ClientExtensions
{
    public static async Task<HttpResponseMessage> SendAsync(
        this HttpClient httpClient,
        HttpRequestMessageTemplate template,
        CancellationToken cancellationToken
    ) =>
        await httpClient.SendAsync(
                template.Compile(),
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);
}
