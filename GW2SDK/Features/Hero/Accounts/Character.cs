using GuildWars2.Hero.Builds;
using GuildWars2.Hero.Crafting;
using GuildWars2.Hero.Crafting.Disciplines;
using GuildWars2.Hero.Equipment.Templates;
using GuildWars2.Hero.Inventories;
using GuildWars2.Hero.Training;

namespace GuildWars2.Hero.Accounts;

/// <summary>Information about a player character.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record Character
{
    /// <summary>The name of the current character.</summary>
    /// <remarks>This can be changed later with a Black Lion contract.</remarks>
    public required string Name { get; init; }

    /// <summary>The race selected during creation of the current character.</summary>
    public required RaceName Race { get; init; }

    /// <summary>Whether the character is male or female.</summary>
    /// <remarks>This can be changed later with a Black Lion contract.</remarks>
    public required Gender Gender { get; init; }

    /// <summary>Additional facts about the current character that did not fit anywhere else.</summary>
    public required IReadOnlyCollection<CharacterFlag> Flags { get; init; }

    /// <summary>The profession name of the current character.</summary>
    public required ProfessionName Profession { get; init; }

    /// <summary>The character's level.</summary>
    public required int Level { get; init; }

    /// <summary>The current guild, or an empty string if the character is not currently representing a guild.</summary>
    public required string GuildId { get; init; }

    /// <summary>The amount of time played as the current character.</summary>
    public required TimeSpan Age { get; init; }

    /// <summary>The date and time when this information was last updated.</summary>
    /// <remarks>This is roughly equal to the last time played as the current character.</remarks>
    public required DateTimeOffset LastModified { get; init; }

    /// <summary>The date and time when the current character was created.</summary>
    public required DateTimeOffset Created { get; init; }

    /// <summary>The number of times the current character was fully defeated.</summary>
    public required int Deaths { get; init; }

    public required IReadOnlyCollection<CraftingDiscipline> CraftingDisciplines { get; init; }

    /// <summary>The selected title ID of the current character.</summary>
    public required int? TitleId { get; init; }

    /// <summary>The IDs of the answers to backstory questions that were selected during creation of the current character.</summary>
    public required IReadOnlyCollection<string> Backstory { get; init; }

    /// <summary>The trained WvW abilities and their rank.</summary>
    public required IReadOnlyCollection<WvwAbility>? WvwAbilities { get; init; }

    /// <summary>The number of build templates available to the current character.</summary>
    public required int? BuildTemplatesCount { get; init; }

    /// <summary>The active build template number on the current character.</summary>
    /// <remarks>This starts counting at 1, do not use as a collection index. Expect API updates to be delayed by a few minutes
    /// when switching the active build template in the game.</remarks>
    public required int? ActiveBuildTemplateNumber { get; init; }

    /// <summary>All the build templates of the current character.</summary>
    public required IReadOnlyList<BuildTemplate>? BuildTemplates { get; init; }

    /// <summary>The number of equipment templates available to the current character.</summary>
    public required int? EquipmentTemplatesCount { get; init; }

    /// <summary>The active equipment template number on the current character.</summary>
    /// <remarks>This starts counting at 1, do not use as a collection index. Expect API updates to be delayed by a few minutes
    /// when switching the active equipment template in the game.</remarks>
    public required int? ActiveEquipmentTemplateNumber { get; init; }

    /// <summary>All the items equipped by the current character. This includes items from all equipment templates, not just
    /// the current template.</summary>
    public required IReadOnlyList<EquipmentItem>? EquippedItems { get; init; }

    /// <summary>All the equipment templates of the current character.</summary>
    public required IReadOnlyList<EquipmentTemplate>? EquipmentTemplates { get; init; }

    /// <summary>The IDs of the recipes that the current character has unlocked.</summary>
    /// <summary>This includes unlocked recipes that are unavailable to the character's active crafting disciplines.</summary>
    public required IReadOnlyCollection<int>? Recipes { get; init; }

    /// <summary>The current character's hero point progression.</summary>
    public required IReadOnlyCollection<TrainingProgress>? Training { get; init; }

    /// <summary>The current character's bags, sorted by in-game order. Enumerated values can contain <c>null</c> when some bag
    /// expansion slots are empty.</summary>
    public required IReadOnlyCollection<Bag?>? Bags { get; init; }
}
