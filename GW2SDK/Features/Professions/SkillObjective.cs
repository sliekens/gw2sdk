using JetBrains.Annotations;

namespace GuildWars2.Professions;

[PublicAPI]
public sealed record SkillObjective : TrainingObjective
{
    public required int SkillId { get; init; }
}
