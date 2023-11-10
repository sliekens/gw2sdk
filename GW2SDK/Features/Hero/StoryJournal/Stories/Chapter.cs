namespace GuildWars2.Hero.StoryJournal.Stories;

[PublicAPI]
[DataTransferObject]
public sealed record Chapter
{
    public required string Name { get; init; }
}
