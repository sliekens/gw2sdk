﻿using JetBrains.Annotations;

namespace GW2SDK.Accounts;

[PublicAPI]
public sealed record CraftingDiscipline
{
    /// <summary>The name of the current crafting discipline.</summary>
    public required CraftingDisciplineName Discipline { get; init; }

    /// <summary>The level of progression.</summary>
    public required int Rating { get; init; }

    /// <summary>Whether a character has access to the current discipline's station.</summary>
    public required bool Active { get; init; }
}
