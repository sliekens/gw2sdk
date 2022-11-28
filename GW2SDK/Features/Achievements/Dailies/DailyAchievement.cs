using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Achievements.Dailies;

[PublicAPI]
[DataTransferObject]
public sealed record DailyAchievement
{
    public required int Id { get; init; }

    public required LevelRequirement Level { get; init; }

    public required ProductRequirement? RequiredAccess { get; init; }
}
