using System;
using System.Net.Http;
using GW2SDK.Accounts.BuildStorage;
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

    public HomeQuery Home => new(httpClient);

    public InventoryQuery Inventory => new(httpClient);

    public ItemsQuery Items => new(httpClient);

    public ItemStatsQuery ItemStats => new(httpClient);

    public MailCarriersQuery MailCarriers => new(httpClient);

    public MapsQuery Maps => new(httpClient);

    public MasteriesQuery Masteries => new(httpClient);

    public MetaQuery Meta => new(httpClient);

    public MountsQuery Mounts => new(httpClient);

    public ProfessionsQuery Professions => new(httpClient);

    public QuaggansQuery Quaggans => new(httpClient);

    public RacesQuery Races => new(httpClient);

    public SkillsQuery Skills => new(httpClient);

    public SpecializationsQuery Specializations => new(httpClient);

    public StoryQuery Story => new(httpClient);

    public TraitsQuery Traits => new(httpClient);

    public WalletQuery Wallet => new(httpClient);

    public WardrobeQuery Wardrobe => new(httpClient);

    public WorldBossesQuery WorldBosses => new(httpClient);

    public TokenProvider TokenProvider => new(httpClient);
}
