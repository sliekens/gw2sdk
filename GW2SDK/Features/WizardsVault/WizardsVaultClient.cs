using GuildWars2.WizardsVault.AstralRewards;
using GuildWars2.WizardsVault.Http;

namespace GuildWars2.WizardsVault;

/// <summary>Provides query methods for the Wizard's Vault (daily, weekly and special objectives and Astral Rewards).</summary>
[PublicAPI]
public sealed class WizardsVaultClient
{
    private readonly HttpClient httpClient;

    /// <summary>Initializes a new instance of the <see cref="WizardsVaultClient" /> class.</summary>
    /// <param name="httpClient">The HTTP client used for making API requests.</param>
    public WizardsVaultClient(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/account/wizardsvault/listings

    public Task<(HashSet<PurchasedAstralReward> Value, MessageContext Context)> GetPurchasedAstralRewards(
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        PurchasedAstralRewardsRequest request = new()
        {
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion v2/account/wizardsvault/listings
}
