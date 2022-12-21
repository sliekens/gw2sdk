using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GuildWars2.Accounts;

[PublicAPI]
public sealed class CharacterByNameRequest : IHttpRequest<IReplica<Character>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/characters")
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
                    Arguments = new QueryBuilder
                    {
                        { "id", CharacterName },
                        { "v", SchemaVersion.Recommended }
                    }
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        return new Replica<Character>
        {
            Value = json.RootElement.GetCharacter(MissingMemberBehavior),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
