using System.ComponentModel;
using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>The type of damage dealt by a weapon. It is a purely visual effect and does not affect the damage calculation.</summary>
[DefaultValue(Physical)]
[JsonConverter(typeof(DamageTypeJsonConverter))]
public enum DamageType
{
    /// <summary>The default damage type of most weapons, which does not have a special visual effect.</summary>
    Physical,

    /// <summary>Creatures that die from a Choking attack go through a suffocation animation.</summary>
    Choking,

    /// <summary>Creatures that die from a Fire attack will collapse in a flaming heap.</summary>
    Fire,

    /// <summary>Creatures that die from an Ice attack will become pale and stiff before collapsing.</summary>
    Ice,

    /// <summary>Creatures that die from a Lightning attack stiffen and shake uncontrollably, then collapse.</summary>
    Lightning
}
