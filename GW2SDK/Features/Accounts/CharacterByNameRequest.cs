using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Accounts;

[PublicAPI]
public sealed class CharacterByNameRequest : IHttpRequest<IReplica<Character>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/characters")
    {
        AcceptEncoding = "gzip"
    };

    public CharacterByNameRequest(string characterName)
    {
        CharacterName = characterName;
    }

    public string CharacterName { get; }

    public string? AccessToken { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplica<Character>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    BearerToken = AccessToken,
                    Arguments = new QueryBuilder { { "id", CharacterName } }
                },
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        return new Replica<Character>(
            response.Headers.Date.GetValueOrDefault(),
            json.RootElement.GetCharacter(MissingMemberBehavior),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
