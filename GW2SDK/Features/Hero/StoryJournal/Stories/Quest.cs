namespace GuildWars2.Hero.StoryJournal.Stories;

[PublicAPI]
[DataTransferObject]
public sealed record Quest
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required int Level { get; init; }

    public required int Story { get; init; }

    public required IReadOnlyCollection<Goal> Goals { get; init; }
}
