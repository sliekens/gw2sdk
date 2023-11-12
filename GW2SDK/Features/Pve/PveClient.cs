using GuildWars2.Pve.Dungeons;
using GuildWars2.Pve.Home;
using GuildWars2.Pve.MapChests;
using GuildWars2.Pve.Pets;
using GuildWars2.Pve.Raids;
using GuildWars2.Pve.SuperAdventureBox;
using GuildWars2.Pve.WorldBosses;

namespace GuildWars2.Pve;

/// <summary>Provides query methods for things you can find in the Hero panel.</summary>
[PublicAPI]
public sealed class PveClient
{
    private readonly HttpClient httpClient;

    public PveClient(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        this.httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    /// <inheritdoc cref="DungeonsClient" />
    public DungeonsClient Dungeons => new(httpClient);

    /// <inheritdoc cref="HomeClient" />
    public HomeClient Home => new(httpClient);

    /// <inheritdoc cref="MapChestsClient" />
    public MapChestsClient MapChests => new(httpClient);

    /// <inheritdoc cref="PetsClient" />
    public PetsClient Pets => new(httpClient);

    /// <inheritdoc cref="RaidsClient" />
    public RaidsClient Raids => new(httpClient);

    /// <inheritdoc cref="SuperAdventureBoxClient" />
    public SuperAdventureBoxClient SuperAdventureBox => new(httpClient);

    /// <inheritdoc cref="WorldBossesClient" />
    public WorldBossesClient WorldBosses => new(httpClient);
}
