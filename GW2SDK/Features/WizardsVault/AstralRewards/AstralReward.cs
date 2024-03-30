using GuildWars2.Chat;

namespace GuildWars2.WizardsVault.AstralRewards;

/// <summary>Information about an astral reward.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record AstralReward
{
    /// <summary>The reward ID.</summary>
    public required int Id { get; init; }

    /// <summary>The ID of the item that can be purchased.</summary>
    public required int ItemId { get; init; }

    /// <summary>The number of items received upon purchase.</summary>
    public required int ItemCount { get; init; }

    /// <summary>The kind of reward.</summary>
    public required Extensible<RewardKind> Kind { get; init; }

    /// <summary>The Astral Acclaim cost of the reward.</summary>
    public required int Cost { get; init; }

    /// <summary>Gets an item chat link object for this astral reward.</summary>
    /// <returns>The chat link as an object.</returns>
    public ItemLink GetChatLink() =>
        new()
        {
            ItemId = ItemId,
            Count = ItemCount
        };
}
