namespace GuildWars2.Hero.StoryJournal.Stories;

[PublicAPI]
[DataTransferObject]
public sealed record Goal
{
    public required string Active { get; init; }

    public required string Complete { get; init; }
}
