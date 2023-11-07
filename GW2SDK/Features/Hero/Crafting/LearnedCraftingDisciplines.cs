namespace GuildWars2.Hero.Crafting;

[PublicAPI]
public sealed record LearnedCraftingDisciplines
{
    /// <summary>The collection of learned crafting disciplines.</summary>
    public required IReadOnlyCollection<CraftingDiscipline> Disciplines { get; init; }
}
