namespace GuildWars2.Items;

[PublicAPI]
[Inheritable]
public record Gizmo : Item
{
    public required int? GuildUpgradeId { get; init; }
}
