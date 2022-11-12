using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Achievements.Dailies;

[PublicAPI]
[DataTransferObject]
public sealed record DailyAchievement
{
    public required int Id { get; init; }

    public required LevelRequirement Level { get; init; }

    public required ProductRequirement? RequiredAccess { get; init; }
}
