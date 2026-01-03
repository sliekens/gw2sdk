using System.ComponentModel;
using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment;

/// <summary>The material of an armor piece, which determines its appearance.</summary>
[DefaultValue(None)]
[JsonConverter(typeof(MaterialJsonConverter))]
public enum Material
{
    /// <summary>No specific material or unknown material.</summary>
    None,

    /// <summary>Cloth armor.</summary>
    Cloth,

    /// <summary>Leather armor.</summary>
    Leather,

    /// <summary>Metal armor.</summary>
    Metal,

    /// <summary>Fur armor.</summary>
    Fur
}
