namespace GuildWars2.Items;

[PublicAPI]
[Inheritable]
public record Gizmo : Item
{
    public required IReadOnlyCollection<int>? VendorIds { get; init; }

    public required int? GuildUpgradeId { get; init; }
}
