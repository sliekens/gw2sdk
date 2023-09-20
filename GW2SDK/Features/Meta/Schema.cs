namespace GuildWars2.Meta;

[PublicAPI]
[DataTransferObject]
public sealed record Schema
{
    public required string Version { get; init; }

    public required string Description { get; init; }
}
