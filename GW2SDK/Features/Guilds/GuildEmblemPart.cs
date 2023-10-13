namespace GuildWars2.Guilds;

[PublicAPI]
[DataTransferObject]
public sealed record GuildEmblemPart
{
    public required int Id { get; init; }
    public required IReadOnlyList<int> Colors { get; init; }
}
