using System.ComponentModel;
using System.Text.Json.Serialization;

namespace GuildWars2.Hero;

/// <summary>The body types of characters.</summary>
[DefaultValue(None)]
[JsonConverter(typeof(BodyTypeJsonConverter))]
public enum BodyType
{
    /// <summary>No specific body type or unknown body type.</summary>
    None,

    /// <summary>Female body type.</summary>
    Female,

    /// <summary>Male body type.</summary>
    Male
}
