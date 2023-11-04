﻿using GuildWars2.Http;

namespace GuildWars2.SuperAdventureBox.Http;

internal sealed class
    SuperAdventureBoxProgressRequest : IHttpRequest2<SuperAdventureBoxProgress>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/characters/:id/sab")
    {
        AcceptEncoding = "gzip",
        Arguments = new QueryBuilder { { "v", SchemaVersion.Recommended } }
    };

    public SuperAdventureBoxProgressRequest(string characterName)
    {
        CharacterName = characterName;
    }

    public string CharacterName { get; }

    public required string? AccessToken { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(SuperAdventureBoxProgress Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
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
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetSuperAdventureBoxProgress(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
