using System.Globalization;
using GuildWars2.Http;

namespace GuildWars2.Meta.Http;

internal sealed class AssetCdnBuildRequest : IHttpRequest<Replica<Build>>
{
    public async Task<Replica<Build>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        return await Latest().ConfigureAwait(false)
            ?? await Latest64().ConfigureAwait(false)
            ?? throw new InvalidOperationException("Missing value.");

        async Task<Replica<Build>?> Latest()
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
                return null;
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
            return new Replica<Build>
            {
                Value = value,
                Date = response.Headers.Date.GetValueOrDefault(),
                Expires = response.Content.Headers.Expires.GetValueOrDefault(),
                LastModified = response.Content.Headers.LastModified.GetValueOrDefault(),
                ResultContext = null,
                PageContext = null
            };
        }

        async Task<Replica<Build>?> Latest64()
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
                return null;
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
            return new Replica<Build>
            {
                Value = value,
                Date = response.Headers.Date.GetValueOrDefault(),
                Expires = response.Content.Headers.Expires.GetValueOrDefault(),
                LastModified = response.Content.Headers.LastModified.GetValueOrDefault(),
                ResultContext = null,
                PageContext = null
            };
        }
    }
}
