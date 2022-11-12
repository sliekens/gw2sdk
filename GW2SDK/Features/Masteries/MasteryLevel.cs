using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Masteries;

[PublicAPI]
[DataTransferObject]
public sealed record MasteryLevel
{
    public required string Name { get; init; }

    public required string Description { get; init; }

    public required string Instruction { get; init; }

    public required string Icon { get; init; }

    public required int PointCost { get; init; }

    public required int ExperienceCost { get; init; }
}
