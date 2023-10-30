using GuildWars2.Inventories;

namespace GuildWars2.Banking;

/// <summary>Information about the current account's bank.</summary>
[PublicAPI]
public sealed record Bank
{
    /// <summary>The item slots in the bank. Empty slots are represented as <c>null</c>.</summary>
    public required IReadOnlyList<ItemSlot?> Items { get; init; }
}
