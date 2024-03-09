namespace GuildWars2.Items;

[PublicAPI]
public sealed record GenericConsumable : Consumable
{
    public required Effect? Effect { get; init; }

    public required int? GuildUpgradeId { get; init; }
}
