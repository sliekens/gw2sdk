namespace GuildWars2.Stories;

[PublicAPI]
[DataTransferObject]
public sealed record Chapter
{
    public required string Name { get; init; }
}
