namespace GuildWars2.Hero.Crafting;

[PublicAPI]
public sealed record CraftingDiscipline
{
    /// <summary>The name of the current crafting discipline.</summary>
    public required CraftingDisciplineName Discipline { get; init; }

    /// <summary>The level of progression.</summary>
    public required int Rating { get; init; }

    /// <summary>Whether a character has access to the crafting station.</summary>
    public required bool Active { get; init; }
}
