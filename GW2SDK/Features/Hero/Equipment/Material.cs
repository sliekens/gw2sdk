using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment;

/// <summary>The material of an armor piece, which determines its appearance.</summary>
[PublicAPI]
[JsonConverter(typeof(MaterialJsonConverter))]
public enum Material
{
    /// <summary>Cloth armor.</summary>
    Cloth = 1,

    /// <summary>Leather armor.</summary>
    Leather,

    /// <summary>Metal armor.</summary>
    Metal,

    /// <summary>Fur armor.</summary>
    Fur
}
