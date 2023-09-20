namespace GuildWars2.Achievements.Titles;

[PublicAPI]
[DataTransferObject]
public sealed record Title
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required IReadOnlyCollection<int>? Achievements { get; init; }

    public required int? AchievementPointsRequired { get; init; }
}
