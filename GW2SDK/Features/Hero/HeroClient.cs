using GuildWars2.Hero.Accounts;
using GuildWars2.Hero.Achievements;
using GuildWars2.Hero.Banking;
using GuildWars2.Hero.Builds;
using GuildWars2.Hero.Crafting;
using GuildWars2.Hero.Emotes;
using GuildWars2.Hero.Equipment;
using GuildWars2.Hero.Inventories;
using GuildWars2.Hero.Masteries;
using GuildWars2.Hero.StoryJournal;
using GuildWars2.Hero.Training;
using GuildWars2.Hero.Wallet;

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

    /// <inheritdoc cref="AccountClient" />
    public AccountClient Account => new(httpClient);

    /// <inheritdoc cref="AchievementsClient" />
    public AchievementsClient Achievements => new(httpClient);

    /// <inheritdoc cref="BankClient" />
    public BankClient Bank => new(httpClient);

    /// <inheritdoc cref="BuildsClient" />
    public BuildsClient Builds => new(httpClient);

    /// <inheritdoc cref="CraftingClient" />
    public CraftingClient Crafting => new(httpClient);

    /// <inheritdoc cref="EmotesClient" />
    public EmotesClient Emotes => new(httpClient);

    /// <inheritdoc cref="EquipmentClient" />
    public EquipmentClient Equipment=> new(httpClient);

    /// <inheritdoc cref="InventoryClient" />
    public InventoryClient Inventory => new(httpClient);

    /// <inheritdoc cref="MasteriesClient" />
    public MasteriesClient Masteries => new(httpClient);

    /// <inheritdoc cref="StoryJournal" />
    public StoryJournalClient StoryJournal => new(httpClient);

    /// <inheritdoc cref="TrainingClient" />
    public TrainingClient Training => new(httpClient);

    /// <inheritdoc cref="WalletClient" />
    public WalletClient Wallet => new(httpClient);
}
