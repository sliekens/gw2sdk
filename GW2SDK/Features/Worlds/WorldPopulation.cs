using System.ComponentModel;
using System.Text.Json.Serialization;

namespace GuildWars2.Worlds;

/// <summary>The population levels of a world in Guild Wars 2.</summary>
[PublicAPI]
[DefaultValue(None)]
[JsonConverter(typeof(WorldPopulationJsonConverter))]
public enum WorldPopulation
{
    /// <summary>No specific population level or unknown population level.</summary>
    None,

    /// <summary>A world with a low population.</summary>
    Low,

    /// <summary>A world with a medium population.</summary>
    Medium,

    /// <summary>A world with a high population.</summary>
    High,

    /// <summary>A world with a very high population.</summary>
    VeryHigh,

    /// <summary>A world with a full population.</summary>
    Full
}
