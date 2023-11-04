using GuildWars2.Http;

namespace GuildWars2.Banking.Http;

internal sealed class MaterialCategoryByIdRequest : IHttpRequest<Replica<MaterialCategory>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/materials")
    {
        AcceptEncoding = "gzip"
    };

    public MaterialCategoryByIdRequest(int materialCategoryId)
    {
        MaterialCategoryId = materialCategoryId;
    }

    public int MaterialCategoryId { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<MaterialCategory>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", MaterialCategoryId },
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
        var value = json.RootElement.GetMaterialCategory(MissingMemberBehavior);
        return new Replica<MaterialCategory>
        {
            Value = value,
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
