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

    /// <summary>Retrieves the Super Adventure Box progress of a character on the account associated with the access token.
    /// This endpoint is only accessible with a valid access token.</summary>
    /// <param name="characterName">A character name that belongs to the account associated with the access token.</param>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
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
