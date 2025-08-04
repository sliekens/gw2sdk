using System.ComponentModel;
using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Builds.Facts;

/// <summary>The combo finisher that is applied by a skill.</summary>
[DefaultValue(None)]
[JsonConverter(typeof(ComboFinisherNameJsonConverter))]
public enum ComboFinisherName
{
    /// <summary>No specific combo finisher or unknown combo finisher.</summary>
    None,

    /// <summary>Blast finishers creates area effects.</summary>
    Blast,

    /// <summary>Leap finishers apply buffs or cause conditions.</summary>
    Leap,

    /// <summary>Projectile finishers cause a condition to foes when the projectile hits, or apply an effect to an adjacent
    /// ally.</summary>
    Projectile,

    /// <summary>Whirl finishers causes projectiles to be fired in multiple directions, applying effects to targets they hit.</summary>
    Whirl
}
