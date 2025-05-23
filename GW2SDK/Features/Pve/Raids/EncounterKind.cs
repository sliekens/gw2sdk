﻿using System.Text.Json.Serialization;

namespace GuildWars2.Pve.Raids;

/// <summary>The kind of raid encounters.</summary>
[PublicAPI]
[JsonConverter(typeof(EncounterKindJsonConverter))]
public enum EncounterKind
{
    /// <summary>A non-boss encounter, such as an escort event.</summary>
    Checkpoint = 1,

    /// <summary>A boss fight.</summary>
    Boss
}
