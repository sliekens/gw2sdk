using JetBrains.Annotations;

namespace GW2SDK.Professions;

[PublicAPI]
public sealed record TraitObjective : TrainingObjective
{
    public required int TraitId { get; init; }
}
