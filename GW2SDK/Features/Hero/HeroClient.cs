using GuildWars2.Hero.Accounts;
using GuildWars2.Hero.Achievements;
using GuildWars2.Hero.Banking;
using GuildWars2.Hero.Builds;
using GuildWars2.Hero.Crafting;
using GuildWars2.Hero.Dyes;
using GuildWars2.Hero.Emotes;
using GuildWars2.Hero.Equipment;
using GuildWars2.Hero.Finishers;
using GuildWars2.Hero.Gliders;
using GuildWars2.Hero.Inventories;
using GuildWars2.Hero.MailCarriers;
using GuildWars2.Hero.Masteries;
using GuildWars2.Hero.Miniatures;
using GuildWars2.Hero.Mounts;
using GuildWars2.Hero.Novelties;
using GuildWars2.Hero.Outfits;
using GuildWars2.Hero.Stories;
using GuildWars2.Hero.Training;
using GuildWars2.Hero.Wardrobe;

namespace GuildWars2.Hero;

/// <summary>Provides query methods for things you can find in the Hero panel.</summary>
[PublicAPI]
public sealed class HeroClient
{
    private readonly HttpClient httpClient;

    public HeroClient(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        this.httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    /// <inheritdoc cref="AccountQuery" />
    public AccountQuery Account => new(httpClient);


    /// <inheritdoc cref="AchievementsQuery" />
    public AchievementsQuery Achievements => new(httpClient);

    /// <inheritdoc cref="BankQuery" />
    public BankQuery Bank => new(httpClient);

    /// <inheritdoc cref="BuildsQuery" />
    public BuildsQuery Builds => new(httpClient);

    /// <inheritdoc cref="CraftingQuery" />
    public CraftingQuery Crafting => new(httpClient);

    /// <inheritdoc cref="DyesQuery" />
    public DyesQuery Dyes => new(httpClient);

    /// <inheritdoc cref="EmotesQuery" />
    public EmotesQuery Emotes => new(httpClient);

    /// <inheritdoc cref="FinishersQuery" />
    public FinishersQuery Finishers => new(httpClient);

    /// <inheritdoc cref="EquipmentQuery" />
    public EquipmentQuery Equipment => new(httpClient);

    /// <inheritdoc cref="GlidersQuery" />
    public GlidersQuery Gliders => new(httpClient);
    
    /// <inheritdoc cref="InventoryQuery" />
    public InventoryQuery Inventory => new(httpClient);

    /// <inheritdoc cref="MailCarriersQuery" />
    public MailCarriersQuery MailCarriers => new(httpClient);

    /// <inheritdoc cref="MasteriesQuery" />
    public MasteriesQuery Masteries => new(httpClient);

    /// <inheritdoc cref="MiniaturesQuery" />
    public MiniaturesQuery Miniatures => new(httpClient);

    /// <inheritdoc cref="MountsQuery" />
    public MountsQuery Mounts => new(httpClient);

    /// <inheritdoc cref="NoveltiesQuery" />
    public NoveltiesQuery Novelties => new(httpClient);

    /// <inheritdoc cref="OutfitsQuery" />
    public OutfitsQuery Outfits => new(httpClient);

    /// <inheritdoc cref="StoriesQuery" />
    public StoriesQuery Stories => new(httpClient);

    /// <inheritdoc cref="TrainingQuery" />
    public TrainingQuery Training => new(httpClient);

    /// <inheritdoc cref="WardrobeQuery" />
    public WardrobeQuery Wardrobe => new(httpClient);
}
