using GuildWars2.Accounts;
using GuildWars2.Achievements;
using GuildWars2.Banking;
using GuildWars2.Colors;
using GuildWars2.Commerce;
using GuildWars2.Crafting;
using GuildWars2.Currencies;
using GuildWars2.Dungeons;
using GuildWars2.Emblems;
using GuildWars2.Emotes;
using GuildWars2.Equipment;
using GuildWars2.Exploration;
using GuildWars2.Files;
using GuildWars2.Finishers;
using GuildWars2.Gliders;
using GuildWars2.Guilds;
using GuildWars2.Home;
using GuildWars2.Inventories;
using GuildWars2.Items;
using GuildWars2.ItemStats;
using GuildWars2.Legends;
using GuildWars2.MailCarriers;
using GuildWars2.MapChests;
using GuildWars2.Masteries;
using GuildWars2.Meta;
using GuildWars2.Minipets;
using GuildWars2.Mounts;
using GuildWars2.Novelties;
using GuildWars2.Outfits;
using GuildWars2.Pets;
using GuildWars2.Professions;
using GuildWars2.Pvp;
using GuildWars2.Quaggans;
using GuildWars2.Quests;
using GuildWars2.Races;
using GuildWars2.Raids;
using GuildWars2.Skills;
using GuildWars2.Skins;
using GuildWars2.Specializations;
using GuildWars2.Stories;
using GuildWars2.SuperAdventureBox;
using GuildWars2.Tokens;
using GuildWars2.Traits;
using GuildWars2.WorldBosses;
using GuildWars2.Worlds;
using GuildWars2.Wvw;

namespace GuildWars2;

[PublicAPI]
public sealed class Gw2Client
{
    private readonly HttpClient httpClient;

    public Gw2Client(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        this.httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    public AccountsQuery Accounts => new(httpClient);

    public AchievementsQuery Achievements => new(httpClient);

    public BankQuery Bank => new(httpClient);

    public CommerceQuery Commerce => new(httpClient);

    public CraftingQuery Crafting => new(httpClient);

    public DungeonsQuery Dungeons => new(httpClient);

    public DyesQuery Dyes => new(httpClient);

    public EmblemsQuery Emblems => new(httpClient);

    public EmotesQuery Emotes => new(httpClient);

    public EquipmentQuery Equipment => new(httpClient);

    public FilesQuery Files => new(httpClient);

    public FinishersQuery Finishers => new(httpClient);

    public GlidersQuery Gliders => new(httpClient);

    public GuildsQuery Guilds => new(httpClient);

    public HomeQuery Home => new(httpClient);

    public InventoryQuery Inventory => new(httpClient);

    public ItemsQuery Items => new(httpClient);

    public ItemStatsQuery ItemStats => new(httpClient);

    public LegendsQuery Legends => new(httpClient);

    public MailCarriersQuery MailCarriers => new(httpClient);

    public MapChestsQuery MapChests => new(httpClient);

    public MapsQuery Maps => new(httpClient);

    public MasteriesQuery Masteries => new(httpClient);

    public MetaQuery Meta => new(httpClient);

    public MinipetsQuery Minipets => new(httpClient);

    public MountsQuery Mounts => new(httpClient);

    public NoveltiesQuery Novelties => new(httpClient);

    public OutfitsQuery Outfits => new(httpClient);

    public PetsQuery Pets => new(httpClient);

    public ProfessionsQuery Professions => new(httpClient);

    public PvpQuery Pvp => new(httpClient);

    public QuaggansQuery Quaggans => new(httpClient);

    public QuestsQuery Quests => new(httpClient);

    public RacesQuery Races => new(httpClient);

    public SkillsQuery Skills => new(httpClient);

    public SpecializationsQuery Specializations => new(httpClient);

    public SuperAdventureBoxQuery SuperAdventureBox => new(httpClient);

    public RaidsQuery Raids => new(httpClient);

    public StoriesQuery Stories => new(httpClient);

    public TraitsQuery Traits => new(httpClient);

    public WalletQuery Wallet => new(httpClient);

    public WardrobeQuery Wardrobe => new(httpClient);

    public WorldBossesQuery WorldBosses => new(httpClient);

    public WorldsQuery Worlds => new(httpClient);

    public WvwQuery Wvw => new(httpClient);

    public TokenProvider TokenProvider => new(httpClient);
}
