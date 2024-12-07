using System.Text.Json.Serialization;

namespace GuildWars2.WizardsVault;

/// <summary>The kind of rewards available in the Wizard's Vault.</summary>
[PublicAPI]
[JsonConverter(typeof(RewardKindJsonConverter))]
public enum RewardKind
{
    /// <summary>Seasonal rewards such as crafting materials, gold, mystic coins and legendary starter kits. The rewards are
    /// renewed on a quarterly basis.</summary>
    Normal = 1,

    /// <summary>Exclusive rewards such as new weapons and armor skins and a small selection of items previously exclusive to
    /// the gem store. The rewards are renewed on a quarterly basis, previous exclusive rewards are moved to the Legacy Rewards
    /// section where they'll remain available indefinitely, although at a higher price.</summary>
    Featured,

    /// <summary>Exclusive rewards from past seasons. These rewards are available indefinitely, although at a higher price.</summary>
    Legacy
}
