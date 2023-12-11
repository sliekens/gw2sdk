using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Hero.Banking.Http;

internal sealed class MaterialCategoriesByIdsRequest : IHttpRequest<HashSet<MaterialCategory>>
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

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<MaterialCategory> Value, MessageContext Context)> SendAsync(
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
        var value =
            json.RootElement.GetSet(entry => entry.GetMaterialCategory(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
