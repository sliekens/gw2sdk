using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Quests;

[PublicAPI]
[DataTransferObject]
public sealed record Goal
{
    public string Active { get; init; } = "";

    public string Complete { get; init; } = "";
}
