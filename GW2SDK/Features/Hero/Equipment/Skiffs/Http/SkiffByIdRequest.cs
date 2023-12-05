using GuildWars2.Http;

namespace GuildWars2.Hero.Equipment.Skiffs.Http;

internal sealed class SkiffByIdRequest(int skiffId) : IHttpRequest<Skiff>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/skiffs") { AcceptEncoding = "gzip" };

    public int SkiffId { get; } = skiffId;

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(Skiff Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", SkiffId },
                        { "v", SchemaVersion.Recommended }
                    },
                    AcceptLanguage = Language?.Alpha2Code
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetSkiff(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
