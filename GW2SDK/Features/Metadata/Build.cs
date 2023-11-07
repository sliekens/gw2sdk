namespace GuildWars2.Metadata;

[PublicAPI]
[DataTransferObject]
public sealed record Build
{
    public required int Id { get; init; }
}
