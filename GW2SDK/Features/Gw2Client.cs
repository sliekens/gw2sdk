using GuildWars2.Commerce;
using GuildWars2.Dungeons;
using GuildWars2.Exploration;
using GuildWars2.Files;
using GuildWars2.Guilds;
using GuildWars2.Guilds.Emblems;
using GuildWars2.Hero;
using GuildWars2.Home;
using GuildWars2.Items;
using GuildWars2.ItemStats;
using GuildWars2.Legends;
using GuildWars2.MapChests;
using GuildWars2.Metadata;
using GuildWars2.Pets;
using GuildWars2.Pvp;
using GuildWars2.Quaggans;
using GuildWars2.Quests;
using GuildWars2.Races;
using GuildWars2.Raids;
using GuildWars2.SuperAdventureBox;
using GuildWars2.Tokens;
using GuildWars2.WorldBosses;
using GuildWars2.Worlds;
using GuildWars2.Wvw;

namespace GuildWars2;

/// <summary>Provides query methods for the Guild Wars 2 API. This class consists of logical groups containing related sets
/// of APIs. For example, all APIs pertaining to the Hero panel are grouped into <see cref="Hero" /> and all APIs
/// pertaining to the trading post are grouped into <see cref="Commerce" />.</summary>
[PublicAPI]
public sealed class Gw2Client
{
    private readonly HttpClient httpClient;

    public Gw2Client(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        this.httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }
    
    /// <inheritdoc cref="CommerceQuery" />
    public CommerceQuery Commerce => new(httpClient);

    /// <inheritdoc cref="DungeonsQuery" />
    public DungeonsQuery Dungeons => new(httpClient);

    /// <inheritdoc cref="FilesQuery" />
    public FilesQuery Files => new(httpClient);
    
    /// <inheritdoc cref="GuildsQuery" />
    public GuildsQuery Guilds => new(httpClient);

    /// <inheritdoc cref="HeroClient" />
    public HeroClient Hero => new(httpClient);

    /// <inheritdoc cref="HomeQuery" />
    public HomeQuery Home => new(httpClient);

    /// <inheritdoc cref="ItemsQuery" />
    public ItemsQuery Items => new(httpClient);

    /// <inheritdoc cref="ItemStatsQuery" />
    public ItemStatsQuery ItemStats => new(httpClient);

    /// <inheritdoc cref="LegendsQuery" />
    public LegendsQuery Legends => new(httpClient);

    /// <inheritdoc cref="MapChestsQuery" />
    public MapChestsQuery MapChests => new(httpClient);

    /// <inheritdoc cref="MapsQuery" />
    public MapsQuery Maps => new(httpClient);

    /// <inheritdoc cref="MetadataQuery" />
    public MetadataQuery Metadata => new(httpClient);

    /// <inheritdoc cref="PetsQuery" />
    public PetsQuery Pets => new(httpClient);

    /// <inheritdoc cref="PvpQuery" />
    public PvpQuery Pvp => new(httpClient);

    /// <inheritdoc cref="QuaggansQuery" />
    public QuaggansQuery Quaggans => new(httpClient);

    /// <inheritdoc cref="QuestsQuery" />
    public QuestsQuery Quests => new(httpClient);

    /// <inheritdoc cref="RacesQuery" />
    public RacesQuery Races => new(httpClient);

    /// <inheritdoc cref="SuperAdventureBoxQuery" />
    public SuperAdventureBoxQuery SuperAdventureBox => new(httpClient);

    /// <inheritdoc cref="RaidsQuery" />
    public RaidsQuery Raids => new(httpClient);

    /// <inheritdoc cref="WorldBossesQuery" />
    public WorldBossesQuery WorldBosses => new(httpClient);

    /// <inheritdoc cref="WorldsQuery" />
    public WorldsQuery Worlds => new(httpClient);

    /// <inheritdoc cref="WvwQuery" />
    public WvwQuery Wvw => new(httpClient);

    /// <inheritdoc cref="TokenProvider" />
    public TokenProvider TokenProvider => new(httpClient);
}
