using System.ComponentModel;
using System.Text.Json.Serialization;

namespace GuildWars2.Pve.Raids;

/// <summary>The kind of raid encounters.</summary>
[DefaultValue(None)]
[JsonConverter(typeof(EncounterKindJsonConverter))]
public enum EncounterKind
{
    /// <summary>No specific encounter or unknown encounter.</summary>
    None,

    /// <summary>A non-boss encounter, such as an escort event.</summary>
    Checkpoint,

    /// <summary>A boss fight.</summary>
    Boss
}
