using JetBrains.Annotations;

namespace GW2SDK.Professions.Models;

[PublicAPI]
public sealed record TraitObjective : TrainingObjective
{
    public int TraitId { get; init; }
}
