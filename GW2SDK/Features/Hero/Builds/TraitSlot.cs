using System.ComponentModel;
using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Builds;

/// <summary>The trait slots.</summary>
[PublicAPI]
[DefaultValue(None)]
[JsonConverter(typeof(TraitSlotJsonConverter))]
public enum TraitSlot
{
    /// <summary>No specific trait slot or unknown trait slot.</summary>
    None,

    /// <summary>A minor trait which is always active.</summary>
    Minor,

    /// <summary>A major trait which is only active if the player has selected it.</summary>
    Major
}
