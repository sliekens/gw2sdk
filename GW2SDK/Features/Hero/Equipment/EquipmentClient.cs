using GuildWars2.Hero.Equipment.Dyes;
using GuildWars2.Hero.Equipment.Finishers;
using GuildWars2.Hero.Equipment.Gliders;
using GuildWars2.Hero.Equipment.MailCarriers;
using GuildWars2.Hero.Equipment.Miniatures;
using GuildWars2.Hero.Equipment.Mounts;
using GuildWars2.Hero.Equipment.Novelties;
using GuildWars2.Hero.Equipment.Outfits;
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

    /// <inheritdoc cref="DyesQuery" />
    public DyesQuery Dyes => new(httpClient);

    /// <inheritdoc cref="FinishersQuery" />
    public FinishersQuery Finishers => new(httpClient);

    /// <inheritdoc cref="EquipmentQuery" />
    public EquipmentQuery Equipment => new(httpClient);

    /// <inheritdoc cref="GlidersQuery" />
    public GlidersQuery Gliders => new(httpClient);

    /// <inheritdoc cref="MailCarriersQuery" />
    public MailCarriersQuery MailCarriers => new(httpClient);

    /// <inheritdoc cref="MiniaturesQuery" />
    public MiniaturesQuery Miniatures => new(httpClient);

    /// <inheritdoc cref="MountsQuery" />
    public MountsQuery Mounts => new(httpClient);

    /// <inheritdoc cref="NoveltiesQuery" />
    public NoveltiesQuery Novelties => new(httpClient);

    /// <inheritdoc cref="OutfitsQuery" />
    public OutfitsQuery Outfits => new(httpClient);

    /// <inheritdoc cref="WardrobeQuery" />
    public WardrobeQuery Wardrobe => new(httpClient);
}
