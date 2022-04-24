using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Masteries;

[PublicAPI]
[DataTransferObject]
public sealed record MasteryLevel
{
    public string Name { get; init; } = "";

    public string Description { get; init; } = "";

    public string Instruction { get; init; } = "";

    public string Icon { get; init; } = "";

    public int PointCost { get; init; }

    public int ExperienceCost { get; init; }
}
