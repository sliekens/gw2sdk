﻿using System.Text.Json.Serialization;

namespace GuildWars2.Hero;

/// <summary>The attunement of an Elementalist.</summary>
[PublicAPI]
[JsonConverter(typeof(AttunementJsonConverter))]
public enum Attunement
{
    /// <summary>Attunement to Earth.</summary>
    Earth = 1,

    /// <summary>Attunement to Water.</summary>
    Water,

    /// <summary>Attunement to Air.</summary>
    Air,

    /// <summary>Attunement to Fire.</summary>
    Fire
}
