using System.ComponentModel;
using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>The item rarities.</summary>
[DefaultValue(None)]
[JsonConverter(typeof(RarityJsonConverter))]
public enum Rarity
{
    /// <summary>No specific rarity or unknown rarity.</summary>
    None,

    /// <summary>Gray items.</summary>
    Junk,

    /// <summary>White items.</summary>
    Basic,

    /// <summary>Blue items.</summary>
    Fine,

    /// <summary>Green items.</summary>
    Masterwork,

    /// <summary>Yellow items.</summary>
    Rare,

    /// <summary>Orange items.</summary>
    Exotic,

    /// <summary>Pink items.</summary>
    Ascended,

    /// <summary>Purple items</summary>
    Legendary
}
