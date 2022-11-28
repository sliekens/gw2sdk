using System.Collections.Generic;
using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Professions;

[PublicAPI]
[DataTransferObject]
public sealed record Profession
{
    public required ProfessionName Id { get; init; }

    /// <summary>The localized profession name.</summary>
    public required string Name { get; init; }

    public required int Code { get; init; }

    public required string Icon { get; init; }

    public required string IconBig { get; init; }

    public required IReadOnlyCollection<int> Specializations { get; init; }

    public required IDictionary<string, WeaponProficiency> Weapons { get; init; }

    public required IReadOnlyCollection<ProfessionFlag> Flags { get; init; }

    public required IReadOnlyCollection<SkillReference> Skills { get; init; }

    public required IReadOnlyCollection<Training> Training { get; init; }

    public required IDictionary<int, int> SkillsByPalette { get; init; }
}
