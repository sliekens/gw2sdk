using System.Globalization;
using GuildWars2.Http;

namespace GuildWars2.Metadata.Http;

internal sealed class AssetCdnBuildRequest : IHttpRequest<Build>
{
    public async Task<(Build Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        var (value, context) = await Latest().ConfigureAwait(false);
        if (value is null)
        {
            (value, context) = await Latest64().ConfigureAwait(false);
        }

        return (value ?? throw new InvalidOperationException("Missing value."), context);

        async Task<(Build? Value, MessageContext Context)> Latest()
        {
            using var request = new HttpRequestMessage(
                Get,
                "http://assetcdn.101.ArenaNetworks.com/latest/101"
            );
            using var response = await httpClient.SendAsync(
                    request,
                    HttpCompletionOption.ResponseHeadersRead,
                    cancellationToken
                )
                .ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                return (null, new MessageContext(response));
            }

#if NET
            var latest = await response.Content.ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);
#else
            var latest = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
#endif
            var text = latest[..latest.IndexOf(' ')];
            var value = new Build
            {
                Id = int.Parse(text, NumberStyles.None, NumberFormatInfo.InvariantInfo)
            };
            return (value, new MessageContext(response));
        }

        async Task<(Build? Value, MessageContext Context)> Latest64()
        {
            using var request = new HttpRequestMessage(
                Get,
                "http://assetcdn.101.ArenaNetworks.com/latest64/101"
            );
            using var response = await httpClient.SendAsync(
                    request,
                    HttpCompletionOption.ResponseHeadersRead,
                    cancellationToken
                )
                .ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                return (null, new MessageContext(response));
            }

#if NET
            var latest64 = await response.Content.ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);
#else
            var latest64 = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
#endif
            var text = latest64[..latest64.IndexOf(' ')];
            var value = new Build
            {
                Id = int.Parse(text, NumberStyles.None, NumberFormatInfo.InvariantInfo)
            };
            return (value, new MessageContext(response));
        }
    }
}
