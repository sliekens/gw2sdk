using GuildWars2.Hero.Builds.Facts;

namespace GuildWars2.Hero.Builds;

/// <summary>Information about a specialization trait. Traits are passive abilities that modify and enhance skills and
/// provide bonuses to damage and attributes. Some traits add new passive skills which are activated indirectly. For
/// example, the Engineer trait "Grenadier" adds a passive skill "Lesser Grenade Barrage" that is activated when the player
/// uses a heal skill.</summary>
[DataTransferObject]
public sealed record Trait
{
    /// <summary>The trait ID.</summary>
    public required int Id { get; init; }

    /// <summary>The trait tier as a value from 1 to 3 (Adept, Master, Grandmaster) or 0 if it is a weapon trait linked to an
    /// elite specialization weapon.</summary>
    public required int Tier { get; init; }

    /// <summary>The display order of the trait in the build template panel.</summary>
    public required int Order { get; init; }

    /// <summary>The name of the trait as it appears in the tooltip.</summary>
    public required string Name { get; init; }

    /// <summary>The description of the trait as it appears in the tooltip.</summary>
    public required string Description { get; init; }

    /// <summary>Indicates whether it is a minor trait (always active) or a major trait (only active if the trait is selected).</summary>
    public required Extensible<TraitSlot> Slot { get; init; }

    /// <summary>The list of trait behaviors. For example, if the current trait grants a boon, this list will contain a
    /// <see cref="Buff" /> fact describing the boon. The list type is abstract, the derived types are documented in the Facts
    /// namespace.</summary>
    public required IReadOnlyList<Fact>? Facts { get; init; }

    /// <summary>Some specialization traits can alter this trait's <see cref="Facts" />, modifying their behavior or adding new
    /// behaviors. This list contains the overrides that apply when a certain trait is equipped.</summary>
    public required IReadOnlyList<TraitedFact>? TraitedFacts { get; init; }

    /// <summary>The IDs of the skills which may be cast by this trait.</summary>
    public required IReadOnlyList<Skill>? Skills { get; init; }

    /// <summary>The ID of the specialization this trait belongs to.</summary>
    public required int SpezializationId { get; init; }

    /// <summary>The URL of the trait icon.</summary>
    public required Uri IconUrl { get; init; }
}
