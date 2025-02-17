﻿using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Builds.Facts;

/// <summary>The combo field (area effect) that is created by a skill.</summary>
[PublicAPI]
[JsonConverter(typeof(ComboFieldNameJsonConverter))]
public enum ComboFieldName
{
    /// <summary>Dark fields whose combos cause Blindness or Life Stealing.</summary>
    Dark = 1,

    /// <summary>Ethereal fields whose combos apply Chaos Aura to allies and cause Confusion to foes.</summary>
    Ethereal,

    /// <summary>Fire fields whose combos apply Might or Fire Aura to allies and cause Burning to foes.</summary>
    Fire,

    /// <summary>Ice fields whose combos apply Frost Aura to allies and cause Chilled to foes.</summary>
    Ice,

    /// <summary>Light fields whose combos remove conditions from allies.</summary>
    Light,

    /// <summary>Lightning fields whose combos apply Swiftness to allies and cause Dazed or Vulnerability to foes.</summary>
    Lightning,

    /// <summary>Poison fields whose combos cause Weakened or Poisoned to foes.</summary>
    Poison,

    /// <summary>Smoke fields whose combos apply Stealth to allies and cause Blindness to foes.</summary>
    Smoke,

    /// <summary>Water fields whose combos heal or apply Regeneration to allies.</summary>
    Water
}
