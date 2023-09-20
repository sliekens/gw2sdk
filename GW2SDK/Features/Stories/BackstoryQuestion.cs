namespace GuildWars2.Stories;

[PublicAPI]
[DataTransferObject]
public sealed record BackstoryQuestion
{
    public required int Id { get; init; }

    public required string Title { get; init; }

    public required string Description { get; init; }

    public required IReadOnlyCollection<string> Answers { get; init; }

    public required int Order { get; init; }

    public required IReadOnlyCollection<RaceName>? Races { get; init; }

    public required IReadOnlyCollection<ProfessionName>? Professions { get; init; }
}
