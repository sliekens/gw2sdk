using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Builds;

/// <summary>Transformations related to specializations.</summary>
[PublicAPI]
[JsonConverter(typeof(TransformationJsonConverter))]
public enum Transformation
{
    /// <summary>Druid's Celestial Avatar.</summary>
    CelestialAvatar = 1
}
