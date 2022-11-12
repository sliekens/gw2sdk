using JetBrains.Annotations;

namespace GW2SDK.Professions;

[PublicAPI]
public sealed record SkillObjective : TrainingObjective
{
    public required int SkillId { get; init; }
}
