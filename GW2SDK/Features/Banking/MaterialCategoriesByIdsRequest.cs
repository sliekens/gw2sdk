using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Banking;

[PublicAPI]
public sealed class
    MaterialCategoriesByIdsRequest : IHttpRequest<Replica<HashSet<MaterialCategory>>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/materials")
    {
        AcceptEncoding = "gzip"
    };

    public MaterialCategoriesByIdsRequest(IReadOnlyCollection<int> materialCategoriesIds)
    {
        Check.Collection(materialCategoriesIds);
        MaterialCategoriesIds = materialCategoriesIds;
    }

    public IReadOnlyCollection<int> MaterialCategoriesIds { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<HashSet<MaterialCategory>>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", MaterialCategoriesIds },
                        { "v", SchemaVersion.Recommended }
                    },
                    AcceptLanguage = Language?.Alpha2Code
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        return new Replica<HashSet<MaterialCategory>>
        {
            Value =
                json.RootElement.GetSet(
                    entry => entry.GetMaterialCategory(MissingMemberBehavior)
                ),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
