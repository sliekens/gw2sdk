using GuildWars2.Hero;

namespace GuildWars2.Items;

[PublicAPI]
[DataTransferObject]
public sealed record InfixUpgrade
{
    public required int AttributeCombinationId { get; init; }

    public required IDictionary<AttributeName, int> Attributes { get; init; }

    public required Buff? Buff { get; init; }
}
