using GuildWars2.Hero.Achievements;
using GuildWars2.Hero.Builds;
using GuildWars2.Hero.Equipment;
using GuildWars2.Hero.Professions;

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

    /// <inheritdoc cref="AchievementsQuery" />
    public AchievementsQuery Achievements => new(httpClient);

    /// <inheritdoc cref="BuildsQuery" />
    public BuildsQuery Builds => new(httpClient);

    /// <inheritdoc cref="EquipmentQuery" />
    public EquipmentQuery Equipment => new(httpClient);

    /// <inheritdoc cref="ProfessionsQuery" />
    public ProfessionsQuery Professions => new(httpClient);
}
