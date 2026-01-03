using System.ComponentModel;
using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Novelties;

/// <summary>The novelty kinds.</summary>
[DefaultValue(None)]
[JsonConverter(typeof(NoveltyKindJsonConverter))]
public enum NoveltyKind
{
    /// <summary>No specific novelty kind or unknown novelty kind.</summary>
    None,

    /// <summary>Makes the character sit on a chair.</summary>
    Chair,

    /// <summary>Musical instruments.</summary>
    Music,

    /// <summary>Held items such as kites or balloons.</summary>
    HeldItem,

    /// <summary>Various toys such as Bobblehead Laboratory.</summary>
    Miscellaneous,

    /// <summary>Endless tonics.</summary>
    Tonic
}
