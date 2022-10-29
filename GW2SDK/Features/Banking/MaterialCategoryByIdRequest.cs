using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Banking;

[PublicAPI]
public sealed class MaterialCategoryByIdRequest : IHttpRequest<IReplica<MaterialCategory>>
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

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplica<MaterialCategory>> SendAsync(
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
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = json.RootElement.GetMaterialCategory(MissingMemberBehavior);
        return new Replica<MaterialCategory>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
