namespace GuildWars2.Emblems;

[PublicAPI]
[DataTransferObject]
public sealed record Emblem
{
    public required int Id { get; init; }

    public required IReadOnlyCollection<string> Layers { get; init; }
}
