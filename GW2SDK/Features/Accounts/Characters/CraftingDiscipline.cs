using JetBrains.Annotations;

namespace GW2SDK.Accounts.Characters;

[PublicAPI]
public sealed record CraftingDiscipline
{
    /// <summary>The name of the current crafting discipline.</summary>
    public CraftingDisciplineName Discipline { get; init; }

    /// <summary>The level of progression.</summary>
    public int Rating { get; init; }

    /// <summary>Whether a character has access to the current discipline's station.</summary>
    public bool Active { get; init; }
}
