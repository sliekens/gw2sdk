using System.Collections.Generic;
using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.BuildStorage;

[PublicAPI]
[DataTransferObject]
public sealed record Build
{
    /// <summary>The player-chosen name of this build.</summary>
    public required string Name { get; init; }

    public required ProfessionName Profession { get; init; }

    // Always length 3
    public required IReadOnlyCollection<Specialization> Specializations { get; init; }

    public required SkillBar Skills { get; init; }

    public required SkillBar AquaticSkills { get; init; }

    public required PetSkillBar? Pets { get; init; }

    // Always length 2 or missing
    public required IReadOnlyCollection<string?>? Legends { get; init; }

    // Always length 2 or missing
    public required IReadOnlyCollection<string?>? AquaticLegends { get; init; }
}
