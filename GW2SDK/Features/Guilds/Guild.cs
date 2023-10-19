namespace GuildWars2.Guilds;

[PublicAPI]
[DataTransferObject]
public sealed record Guild
{
    public required int Level { get; init; }

    public required string MessageOfTheDay { get; init; }

    public required int Influence { get; init; }

    public required int Aetherium { get; init; }

    public required int Resonance { get; init; }

    public required int Favor { get; init; }

    public required int MemberCount { get; init; }

    public required int MemberCapacity { get; init; }

    public required string Id { get; init; }

    public required string Name { get; init; }

    public required string Tag { get; init; }

    public required GuildEmblem Emblem { get; init; }
}
