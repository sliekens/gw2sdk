using GuildWars2.Authentication;
using GuildWars2.Commerce;
using GuildWars2.Exploration;
using GuildWars2.Files;
using GuildWars2.Guilds;
using GuildWars2.Hero;
using GuildWars2.Items;
using GuildWars2.Metadata;
using GuildWars2.Pve;
using GuildWars2.Pvp;
using GuildWars2.Quaggans;
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

    /// <inheritdoc cref="CommerceClient" />
    public CommerceClient Commerce => new(httpClient);

    /// <inheritdoc cref="ExplorationClient" />
    public ExplorationClient Exploration => new(httpClient);

    /// <inheritdoc cref="FilesClient" />
    public FilesClient Files => new(httpClient);

    /// <inheritdoc cref="GuildsClient" />
    public GuildsClient Guilds => new(httpClient);

    /// <inheritdoc cref="HeroClient" />
    public HeroClient Hero => new(httpClient);

    /// <inheritdoc cref="ItemsClient" />
    public ItemsClient Items => new(httpClient);

    /// <inheritdoc cref="MetadataClient" />
    public MetadataClient Metadata => new(httpClient);

    /// <inheritdoc cref="PveClient" />
    public PveClient Pve => new(httpClient);

    /// <inheritdoc cref="PvpClient" />
    public PvpClient Pvp => new(httpClient);

    /// <inheritdoc cref="QuaggansClient" />
    public QuaggansClient Quaggans => new(httpClient);

    /// <inheritdoc cref="TokenClient" />
    public TokenClient Tokens => new(httpClient);

    /// <inheritdoc cref="WorldsClient" />
    public WorldsClient Worlds => new(httpClient);

    /// <inheritdoc cref="WvwClient" />
    public WvwClient Wvw => new(httpClient);
}
