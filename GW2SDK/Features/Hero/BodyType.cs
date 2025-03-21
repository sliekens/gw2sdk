﻿using System.Text.Json.Serialization;

namespace GuildWars2.Hero;

/// <summary>The body types of characters.</summary>
[PublicAPI]
[JsonConverter(typeof(BodyTypeJsonConverter))]
public enum BodyType
{
    /// <summary>Female body type.</summary>
    Female = 1,

    /// <summary>Male body type.</summary>
    Male
}
