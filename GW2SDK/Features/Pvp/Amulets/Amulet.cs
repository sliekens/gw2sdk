using GuildWars2.Hero;

namespace GuildWars2.Pvp.Amulets;

[PublicAPI]
[DataTransferObject]
public sealed record Amulet
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required string IconHref { get; init; }

    public required IDictionary<CombatAttribute, int> Attributes { get; init; }
}
