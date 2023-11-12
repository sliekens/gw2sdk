using GuildWars2.Hero.Equipment.Dyes;
using GuildWars2.Hero.Equipment.Finishers;
using GuildWars2.Hero.Equipment.Gliders;
using GuildWars2.Hero.Equipment.MailCarriers;
using GuildWars2.Hero.Equipment.Miniatures;
using GuildWars2.Hero.Equipment.Mounts;
using GuildWars2.Hero.Equipment.Novelties;
using GuildWars2.Hero.Equipment.Outfits;
using GuildWars2.Hero.Equipment.Skiffs;
using GuildWars2.Hero.Equipment.Templates;
using GuildWars2.Hero.Equipment.Wardrobe;

namespace GuildWars2.Hero.Equipment;

/// <summary>Provides query methods for things you can find in the Hero panel.</summary>
[PublicAPI]
public sealed class EquipmentClient
{
    private readonly HttpClient httpClient;

    public EquipmentClient(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
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
