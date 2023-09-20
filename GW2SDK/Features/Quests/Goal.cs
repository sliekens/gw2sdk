namespace GuildWars2.Quests;

[PublicAPI]
[DataTransferObject]
public sealed record Goal
{
    public required string Active { get; init; }

    public required string Complete { get; init; }
}
