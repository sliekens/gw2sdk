namespace GuildWars2.Meta;

[PublicAPI]
[DataTransferObject]
public sealed record Route
{
    public required string Path { get; init; }

    public required bool Multilingual { get; init; }

    public required bool RequiresAuthorization { get; init; }

    public required bool Active { get; init; }
}
