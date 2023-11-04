using GuildWars2.Http;

namespace GuildWars2.Builds.Http;

internal sealed class TraitByIdRequest : IHttpRequest<Trait>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/traits")
    {
        AcceptEncoding = "gzip"
    };

    public TraitByIdRequest(int traitId)
    {
        TraitId = traitId;
    }

    public int TraitId { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(Trait Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", TraitId },
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
        var value = json.RootElement.GetTrait(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
