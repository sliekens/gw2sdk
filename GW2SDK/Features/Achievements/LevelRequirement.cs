using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Achievements;

[PublicAPI]
[DataTransferObject]
public sealed record LevelRequirement
{
    public required int Min { get; init; }

    public required int Max { get; init; }
}
