using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Quests;

[PublicAPI]
[DataTransferObject]
public sealed record Goal
{
    public required string Active { get; init; }

    public required string Complete { get; init; }
}
