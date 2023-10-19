namespace GuildWars2.Files;

[PublicAPI]
[DataTransferObject]
public sealed record Asset

{
    public required string Id { get; init; }

    public required string Icon { get; init; }
}
