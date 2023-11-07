namespace GuildWars2.Metadata;

[PublicAPI]
[DataTransferObject]
public sealed record Schema
{
    public required string Version { get; init; }

    public required string Description { get; init; }
}
