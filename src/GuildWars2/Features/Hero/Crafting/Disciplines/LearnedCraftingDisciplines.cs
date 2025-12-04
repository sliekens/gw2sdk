using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Disciplines;

/// <summary>Information about the learned crafting disciplines of a character.</summary>
[DataTransferObject]
[JsonConverter(typeof(LearnedCraftingDisciplinesJsonConverter))]
public sealed record LearnedCraftingDisciplines
{
    /// <summary>The learned crafting disciplines.</summary>
    public required IReadOnlyList<CraftingDiscipline> Disciplines { get; init; }
}
