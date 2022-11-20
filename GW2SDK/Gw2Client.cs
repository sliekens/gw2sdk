using System;
using System.Net.Http;
using GW2SDK.Accounts;
using GW2SDK.Achievements;
using GW2SDK.Armory;
using GW2SDK.Banking;
using GW2SDK.BuildStorage;
using GW2SDK.Colors;
using GW2SDK.Commerce;
using GW2SDK.Crafting;
using GW2SDK.Currencies;
using GW2SDK.Dungeons;
using GW2SDK.Emblems;
using GW2SDK.Emotes;
using GW2SDK.Exploration;
using GW2SDK.Files;
using GW2SDK.Finishers;
using GW2SDK.Gliders;
using GW2SDK.Guilds;
using GW2SDK.Home;
using GW2SDK.Inventories;
using GW2SDK.Items;
using GW2SDK.ItemStats;
using GW2SDK.Legends;
using GW2SDK.MailCarriers;
using GW2SDK.MapChests;
using GW2SDK.Masteries;
using GW2SDK.Meta;
using GW2SDK.Minipets;
using GW2SDK.Mounts;
using GW2SDK.Novelties;
using GW2SDK.Outfits;
using GW2SDK.Pets;
using GW2SDK.Professions;
using GW2SDK.Quaggans;
using GW2SDK.Quests;
using GW2SDK.Races;
using GW2SDK.Raids;
using GW2SDK.Skills;
using GW2SDK.Skins;
using GW2SDK.Specializations;
using GW2SDK.Stories;
using GW2SDK.Tokens;
using GW2SDK.Traits;
using GW2SDK.WorldBosses;
using GW2SDK.Worlds;
using JetBrains.Annotations;

namespace GW2SDK;

[PublicAPI]
public sealed class Gw2Client
{
    private readonly HttpClient httpClient;

    public Gw2Client(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public AccountsQuery Accounts => new(httpClient);

    public AchievementsQuery Achievements => new(httpClient);

    public ArmoryQuery Armory => new(httpClient);

    public BankQuery Bank => new(httpClient);

    public BuildStorageQuery BuildStorage => new(httpClient);

    public CommerceQuery Commerce => new(httpClient);

    public CraftingQuery Crafting => new(httpClient);

    public DungeonsQuery Dungeons => new(httpClient);

    public DyesQuery Dyes => new(httpClient);

    public EmblemsQuery Emblems => new(httpClient);

    public EmotesQuery Emotes => new(httpClient);

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

    public QuaggansQuery Quaggans => new(httpClient);

    public QuestsQuery Quests => new(httpClient);

    public RacesQuery Races => new(httpClient);

    public SkillsQuery Skills => new(httpClient);

    public SpecializationsQuery Specializations => new(httpClient);

    public RaidsQuery Raids => new(httpClient);

    public StoriesQuery Stories => new(httpClient);

    public TraitsQuery Traits => new(httpClient);

    public WalletQuery Wallet => new(httpClient);

    public WardrobeQuery Wardrobe => new(httpClient);

    public WorldBossesQuery WorldBosses => new(httpClient);

    public WorldsQuery Worlds => new(httpClient);

    public TokenProvider TokenProvider => new(httpClient);
}
