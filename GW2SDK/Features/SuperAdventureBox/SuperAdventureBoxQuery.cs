using GuildWars2.SuperAdventureBox.Http;

namespace GuildWars2.SuperAdventureBox;

[PublicAPI]
public sealed class SuperAdventureBoxQuery
{
    private readonly HttpClient http;

    public SuperAdventureBoxQuery(HttpClient http)
    {
        this.http = http ?? throw new ArgumentNullException(nameof(http));
        http.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/characters/:id/sab

    public Task<Replica<SuperAdventureBoxProgress>> GetSuperAdventureBoxProgress(
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
        return request.SendAsync(http, cancellationToken);
    }

    #endregion
}
