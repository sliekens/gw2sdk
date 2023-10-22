namespace GuildWars2.Crafting;

[PublicAPI]
public sealed record LearnedCraftingDisciplines
{
    /// <summary>The collection of learned crafting disciplines.</summary>
    public required IReadOnlyCollection<CraftingDiscipline> Disciplines { get; init; }
}
