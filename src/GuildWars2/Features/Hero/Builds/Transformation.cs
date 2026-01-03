using System.ComponentModel;
using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Builds;

/// <summary>Transformations related to specializations.</summary>
[DefaultValue(None)]
[JsonConverter(typeof(TransformationJsonConverter))]
public enum Transformation
{
    /// <summary>No specific transformation or unknown transformation.</summary>
    None,

    /// <summary>Druid's Celestial Avatar.</summary>
    CelestialAvatar
}
