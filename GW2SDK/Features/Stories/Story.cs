namespace GuildWars2.Stories;

[PublicAPI]
[DataTransferObject]
public sealed record Story
{
    public required int Id { get; init; }

    public required string SeasonId { get; init; }

    public required string Name { get; init; }

    public required string Description { get; init; }

    public required string Timeline { get; init; }

    public required int Level { get; init; }

    public required IReadOnlyCollection<RaceName>? Races { get; init; }

    public required int Order { get; init; }

    public required IReadOnlyCollection<Chapter> Chapters { get; init; }

    public required IReadOnlyCollection<StoryFlag> Flags { get; init; }
}
