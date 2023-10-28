namespace GuildWars2.Achievements.Categories;

[PublicAPI]
[DataTransferObject]
public sealed record AchievementRef
{
    public required int Id { get; init; }

    public required ProductRequirement? RequiredAccess { get; init; }

    public required AchievementFlags Flags { get; init; }

    public required LevelRequirement? Level { get; init; }
}
