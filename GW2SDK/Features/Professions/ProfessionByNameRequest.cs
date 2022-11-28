using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GuildWars2.Professions;

[PublicAPI]
public sealed class ProfessionByNameRequest : IHttpRequest<IReplica<Profession>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/professions")
    {
        AcceptEncoding = "gzip"
    };

    public ProfessionByNameRequest(ProfessionName professionName)
    {
        Check.Constant(professionName, nameof(professionName));
        ProfessionName = professionName;
    }

    public ProfessionName ProfessionName { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplica<Profession>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", ProfessionName.ToString() },
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

        var value = json.RootElement.GetProfession(MissingMemberBehavior);
        return new Replica<Profession>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
