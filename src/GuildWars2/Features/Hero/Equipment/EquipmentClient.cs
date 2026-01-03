using GuildWars2.Hero.Equipment.Dyes;
using GuildWars2.Hero.Equipment.Finishers;
using GuildWars2.Hero.Equipment.Gliders;
using GuildWars2.Hero.Equipment.JadeBots;
using GuildWars2.Hero.Equipment.MailCarriers;
using GuildWars2.Hero.Equipment.Miniatures;
using GuildWars2.Hero.Equipment.Mounts;
using GuildWars2.Hero.Equipment.Novelties;
using GuildWars2.Hero.Equipment.Outfits;
using GuildWars2.Hero.Equipment.Skiffs;
using GuildWars2.Hero.Equipment.Templates;
using GuildWars2.Hero.Equipment.Wardrobe;

namespace GuildWars2.Hero.Equipment;

/// <summary>Provides query methods for APIs related to equipment and cosmetic items. This class consists of logical groups
/// containing related sets of APIs. For example, all APIs pertaining to equipment templates are grouped into
/// <see cref="Templates" /> and all APIs pertaining to armor and weapon skins are grouped into <see cref="Wardrobe" />.</summary>
public sealed class EquipmentClient
{
    private readonly HttpClient httpClient;

    /// <summary>Initializes a new instance of the <see cref="EquipmentClient" /> class.</summary>
    /// <param name="httpClient">The HTTP client used for making API requests.</param>
    public EquipmentClient(HttpClient httpClient)
    {
        ThrowHelper.ThrowIfNull(httpClient);
        this.httpClient = httpClient;
        this.httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    /// <inheritdoc cref="DyesClient" />
    public DyesClient Dyes => new(httpClient);

    /// <inheritdoc cref="FinishersClient" />
    public FinishersClient Finishers => new(httpClient);

    /// <inheritdoc cref="EquipmentTemplatesClient" />
    public EquipmentTemplatesClient Templates => new(httpClient);

    /// <inheritdoc cref="GlidersClient" />
    public GlidersClient Gliders => new(httpClient);

    /// <inheritdoc cref="JadeBotsClient" />
    public JadeBotsClient JadeBots => new(httpClient);

    /// <inheritdoc cref="MailCarriersClient" />
    public MailCarriersClient MailCarriers => new(httpClient);

    /// <inheritdoc cref="MiniaturesClient" />
    public MiniaturesClient Miniatures => new(httpClient);

    /// <inheritdoc cref="MountsClient" />
    public MountsClient Mounts => new(httpClient);

    /// <inheritdoc cref="NoveltiesClient" />
    public NoveltiesClient Novelties => new(httpClient);

    /// <inheritdoc cref="OutfitsClient" />
    public OutfitsClient Outfits => new(httpClient);

    /// <inheritdoc cref="SkiffsClient" />
    public SkiffsClient Skiffs => new(httpClient);

    /// <inheritdoc cref="WardrobeClient" />
    public WardrobeClient Wardrobe => new(httpClient);
}
