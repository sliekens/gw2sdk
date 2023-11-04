namespace GuildWars2.Guilds.Bank;

/// <summary>An item in the guild vault.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record GuildBankTab
{
    /// <summary>ID of the guild upgrade that granted access to this section of the guild vault.</summary>
    public required int UpgradeId { get; init; }

    /// <summary>The number of slots in this section of the guild vault.</summary>
    public required int Size { get; init; }

    /// <summary>The number of coins deposited in this section of the guild vault.</summary>
    public required Coin Coins { get; init; }

    /// <summary>The description set for this section of the guild's vault.</summary>
    public required string Note { get; init; }

    /// <summary>The ordered list of item slots. Empty slots are represented as <c>null</c>.</summary>
    public required IReadOnlyList<GuildBankSlot?> Inventory { get; init; }
}
