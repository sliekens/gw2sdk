using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Masteries;

[PublicAPI]
[DataTransferObject]
public sealed record MasteryProgress
{
    public required int Id { get; init; }

    public required int Level { get; init; }
}
