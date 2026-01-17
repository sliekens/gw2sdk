using GuildWars2.Hero.Inventories;

namespace GuildWars2.Hero.Banking;

/// <summary>Information about the current account's bank.</summary>
[DataTransferObject]
public sealed record Bank
{
    /// <summary>The item slots in the bank. Empty slots are represented as <c>null</c>.</summary>
    public required IImmutableValueList<ItemSlot?> Items { get; init; }
}
