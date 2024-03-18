namespace GuildWars2.Items;

/// <summary>Information about a miniature item, which can be used to summon the miniature, or consumed to permanently add
/// the miniature to the account.</summary>
[PublicAPI]
public sealed record Miniature : Item
{
    /// <summary>The ID of the miniature which is summoned when used.</summary>
    public required int MiniatureId { get; init; }
}
