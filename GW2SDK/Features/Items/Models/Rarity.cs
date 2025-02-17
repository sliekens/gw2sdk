﻿using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>The item rarities.</summary>
[PublicAPI]
[JsonConverter(typeof(RarityJsonConverter))]
public enum Rarity
{
    /// <summary>Gray items.</summary>
    Junk = 1,

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
