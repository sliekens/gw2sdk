using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Builds;

/// <summary>The trait slots.</summary>
[PublicAPI]
[JsonConverter(typeof(TraitSlotJsonConverter))]
public enum TraitSlot
{
    /// <summary>A minor trait which is always active.</summary>
    Minor = 1,

    /// <summary>A major trait which is only active if the player has selected it.</summary>
    Major
}
