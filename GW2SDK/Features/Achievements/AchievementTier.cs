using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Achievements;

[PublicAPI]
[DataTransferObject]
public sealed record AchievementTier
{
    public required int Count { get; init; }

    public required int Points { get; init; }
}
