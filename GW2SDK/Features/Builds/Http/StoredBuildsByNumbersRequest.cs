using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Builds.Http;

internal sealed class StoredBuildsByNumbersRequest : IHttpRequest<IReadOnlyList<Build>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/account/buildstorage") { AcceptEncoding = "gzip" };

    public StoredBuildsByNumbersRequest(IReadOnlyCollection<int> slotNumbers)
    {
        SlotNumbers = slotNumbers;
    }

    public IReadOnlyCollection<int> SlotNumbers { get; }

    public required string? AccessToken { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(IReadOnlyList<Build> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", SlotNumbers },
                        { "v", SchemaVersion.Recommended }
                    },
                    BearerToken = AccessToken
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        var value = json.RootElement.GetList(entry => entry.GetBuild(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
