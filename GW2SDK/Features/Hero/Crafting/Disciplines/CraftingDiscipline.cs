﻿using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Disciplines;

/// <summary>Information about a character's crafting discipline.</summary>
[PublicAPI]
[DataTransferObject]
[JsonConverter(typeof(CraftingDisciplineJsonConverter))]
public sealed record CraftingDiscipline
{
    /// <summary>The name of the current crafting discipline.</summary>
    public required Extensible<CraftingDisciplineName> Discipline { get; init; }

    /// <summary>The level of progression.</summary>
    public required int Rating { get; init; }

    /// <summary>Whether a character has access to the crafting station.</summary>
    public required bool Active { get; init; }
}
