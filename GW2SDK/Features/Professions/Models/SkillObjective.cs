using JetBrains.Annotations;

namespace GW2SDK.Professions;

[PublicAPI]
public sealed record SkillObjective : TrainingObjective
{
    public int SkillId { get; init; }
}