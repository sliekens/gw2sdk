using GuildWars2.Http;

namespace GuildWars2.Stories;

[PublicAPI]
public sealed class CharacterBackstoryRequest : IHttpRequest<Replica<CharacterBackstory>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/characters/:id/backstory")
    {
        AcceptEncoding = "gzip",
        Arguments = new QueryBuilder
        {
            { "v", SchemaVersion.Recommended }
        }
    };
    public CharacterBackstoryRequest(string characterName)
    {
        CharacterName = characterName;
    }

    public string CharacterName { get; }

    public string? AccessToken { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<CharacterBackstory>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken)
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Path = Template.Path.Replace(":id", CharacterName),
                    BearerToken = AccessToken
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        return new Replica<CharacterBackstory>
        {
            Value = json.RootElement.GetCharacterBackstory(MissingMemberBehavior),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}