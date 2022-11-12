using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Achievements;

[PublicAPI]
[DataTransferObject]
public sealed record ProductRequirement
{
    public required ProductName Product { get; init; }

    public required AccessCondition Condition { get; init; }
}
