using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Accounts.Characters;

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

    public MissingMemberBehavior MissingMemberBehavior { get; set; }

    public async Task<IReplica<Character>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        QueryBuilder search = new();
        search.Add("id", CharacterName);
        var request = Template with
        {
            BearerToken = AccessToken,
            Arguments = search
        };
        using var response = await httpClient.SendAsync(
                request.Compile(),
                HttpCompletionOption.ResponseHeadersRead,
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
