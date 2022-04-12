using GW2SDK.Annotations;

using JetBrains.Annotations;

namespace GW2SDK.Professions;

[PublicAPI]
[Inheritable]
[DataTransferObject]
public record TrainingObjective
{
    public int Cost { get; init; }
}