using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Professions;

[PublicAPI]
[Inheritable]
[DataTransferObject]
public record TrainingObjective
{
    public required int Cost { get; init; }
}
