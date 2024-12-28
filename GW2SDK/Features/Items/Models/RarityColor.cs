using System.Drawing;
using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>The item rarity colors.</summary>
[PublicAPI]
[JsonConverter(typeof(RarityJsonConverter))]
public static class RarityColor
{
    /// <summary>Gray items.</summary>
    public static readonly Color Junk = Color.FromArgb(0x99, 0x99, 0x99);

    /// <summary>White items.</summary>
    public static readonly Color Basic = Color.White;

    /// <summary>Blue items.</summary>
    public static readonly Color Fine = Color.FromArgb(0x55, 0x99, 0xFF);

    /// <summary>Green items.</summary>
    public static readonly Color Masterwork = Color.FromArgb(0x33, 0xCC, 0x11);

    /// <summary>Yellow items.</summary>
    public static readonly Color Rare = Color.FromArgb(0xFF, 0xDD, 0x22);

    /// <summary>Orange items.</summary>
    public static readonly Color Exotic = Color.FromArgb(0xFF, 0xAA, 0x00);

    /// <summary>Pink items.</summary>
    public static readonly Color Ascended = Color.FromArgb(0xFF, 0x44, 0x88);

    /// <summary>Purple items</summary>
    public static readonly Color Legendary = Color.FromArgb(0x99, 0x33, 0xFF);

}
