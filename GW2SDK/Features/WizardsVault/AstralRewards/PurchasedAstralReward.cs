using GuildWars2.Chat;

namespace GuildWars2.WizardsVault.AstralRewards;

/// <summary>Information about a reward in the Wizard's Vault.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record PurchasedAstralReward
{
    /// <summary>The reward ID.</summary>
    public required int Id { get; init; }

    /// <summary>The ID of the item that can be purchased.</summary>
    public required int ItemId { get; init; }

    /// <summary>The number of items received upon purchase.</summary>
    public required int ItemCount { get; init; }

    /// <summary>The kind of reward.</summary>
    public required RewardKind Kind { get; init; }

    /// <summary>The Astral Acclaim cost of the reward.</summary>
    public required int Cost { get; init; }

    /// <summary>The number of times the reward has been purchased during the current season, or <c>null</c> if there is no
    /// limit, then purchases are not counted.</summary>
    public required int? Purchased { get; init; }

    /// <summary>The maximum number of times the reward can be purchased during the current season, or <c>null</c> if there is
    /// no limit.</summary>
    public required int? PurchaseLimit { get; init; }

    /// <summary>The number of times the reward can still be purchased during the current season, or <c>null</c> if there is no
    /// limit.</summary>
    public int? Available => PurchaseLimit - Purchased;

    /// <summary>Gets an item chat link object for this astral reward.</summary>
    /// <returns>The chat link as an object.</returns>
    public ItemLink GetChatLink() =>
        new()
        {
            ItemId = ItemId,
            Count = ItemCount
        };
}
