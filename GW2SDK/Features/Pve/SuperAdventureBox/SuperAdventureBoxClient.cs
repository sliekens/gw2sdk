using GuildWars2.Pve.SuperAdventureBox.Http;

namespace GuildWars2.Pve.SuperAdventureBox;

/// <summary>Provides query methods for Super Adventure Box progress.</summary>
[PublicAPI]
public sealed class SuperAdventureBoxClient
{
    private readonly HttpClient httpClient;

    public SuperAdventureBoxClient(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/characters/:id/sab

    public Task<(SuperAdventureBoxProgress Value, MessageContext Context)>
        GetSuperAdventureBoxProgress(
            string characterId,
            string? accessToken,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
    {
        var request = new SuperAdventureBoxProgressRequest(characterId)
        {
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion
}
