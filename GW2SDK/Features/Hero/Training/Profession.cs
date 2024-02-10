﻿using GuildWars2.Hero.Builds;

namespace GuildWars2.Hero.Training;

/// <summary>Information about a profession.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record Profession
{
    /// <summary>The profession ID.</summary>
    public required ProfessionName Id { get; init; }

    /// <summary>The profession name.</summary>
    public required string Name { get; init; }

    /// <summary>The profession's code which is used in build template chat links.</summary>
    public required int Code { get; init; }

    /// <summary>The URL of the small profession icon.</summary>
    public required string IconHref { get; init; }

    /// <summary>The URL of the large profession icon.</summary>
    public required string BigIconHref { get; init; }

    /// <summary>The IDs of the specializations that belong to this profession.</summary>
    public required IReadOnlyList<int> SpecializationIds { get; init; }

    /// <summary>Information about weapons that can be trained by this profession, the required specialization, and the weapon
    /// skills associated with a weapon.</summary>
    public required IDictionary<WeaponType, WeaponProficiency> Weapons { get; init; }

    /// <summary>Contains various modifiers for the profession.</summary>
    public required ProfessionFlags Flags { get; init; }

    /// <summary>The skills that can be trained by this profession. The list type is abstract, the derived types are documented
    /// in the Skills namespace.</summary>
    public required IReadOnlyList<SkillSummary> Skills { get; init; }

    /// <summary>The skills and specialization training tracks for this profession.</summary>
    public required IReadOnlyList<Training> Training { get; init; }

    /// <summary>The mapping of palette IDs, which are used in build template chat links, to skill IDs. The key is the palette
    /// ID, the value is the skill ID.</summary>
    public required IDictionary<int, int> SkillsByPalette { get; init; }
}
