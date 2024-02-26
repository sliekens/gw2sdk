using GuildWars2.Pve.SuperAdventureBox.Http;

namespace GuildWars2.Pve.SuperAdventureBox;

/// <summary>Provides query methods for Super Adventure Box progress.</summary>
[PublicAPI]
public sealed class SuperAdventureBoxClient
{
    private readonly HttpClient httpClient;

    /// <summary>Initializes a new instance of the <see cref="SuperAdventureBoxClient" /> class.</summary>
    /// <param name="httpClient">The HTTP client used for making API requests.</param>
    public SuperAdventureBoxClient(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/characters/:id/sab

    public Task<(SuperAdventureBoxProgress Value, MessageContext Context)>
        GetSuperAdventureBoxProgress(
            string characterName,
            string? accessToken,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
    {
        var request = new SuperAdventureBoxProgressRequest(characterName)
        {
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion
}
