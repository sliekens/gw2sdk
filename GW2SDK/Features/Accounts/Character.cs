using GuildWars2.Armory;
using GuildWars2.BuildStorage;
using GuildWars2.Crafting;
using GuildWars2.Inventories;
using GuildWars2.Professions;

namespace GuildWars2.Accounts;

[PublicAPI]
[DataTransferObject]
[Scope(Permission.Account, Permission.Characters)]
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
    [Scope(Permission.Progression)]
    public required IReadOnlyCollection<WvwAbility>? WvwAbilities { get; init; }

    /// <summary>The number of build tabs available to the current character.</summary>
    [Scope(Permission.Builds)]
    public required int? BuildTabsUnlocked { get; init; }

    /// <summary>The active build tab of the current character.</summary>
    /// <remarks>This starts counting at 1, do not use as a collection index.</remarks>
    [Scope(Permission.Builds)]
    public required int? ActiveBuildTab { get; init; }

    /// <summary>All the build tabs of the current character.</summary>
    [Scope(Permission.Builds)]
    public required IReadOnlyCollection<BuildTab>? BuildTabs { get; init; }

    /// <summary>The number of equipment tabs available to the current character.</summary>
    [Scope(ScopeRequirement.Any, Permission.Builds, Permission.Inventories)]
    public required int? EquipmentTabsUnlocked { get; init; }

    /// <summary>The active equipment tab of the current character.</summary>
    /// <remarks>This starts counting at 1, do not use as a collection index.
    /// Expect API updates to be delayed by a few minutes when switching the active build tab in the game.</remarks>
    [Scope(ScopeRequirement.Any, Permission.Builds, Permission.Inventories)]
    public required int? ActiveEquipmentTab { get; init; }

    /// <summary>All the equipment in the current character's armory.</summary>
    [Scope(ScopeRequirement.Any, Permission.Builds, Permission.Inventories)]
    public required IReadOnlyCollection<EquipmentItem>? Equipment { get; init; }

    /// <summary>All the equipment tabs of the current character.</summary>
    [Scope(ScopeRequirement.Any, Permission.Builds, Permission.Inventories)]
    public required IReadOnlyCollection<EquipmentTab>? EquipmentTabs { get; init; }

    /// <summary>The IDs of the recipes that the current character has unlocked.</summary>
    /// <summary>This includes unlocked recipes that are unavailable to the character's active crafting disciplines.</summary>
    [Scope(Permission.Inventories)]
    public required IReadOnlyCollection<int>? Recipes { get; init; }

    /// <summary>The current character's hero point progression.</summary>
    [Scope(Permission.Builds)]
    public required IReadOnlyCollection<TrainingProgress>? Training { get; init; }

    /// <summary>The current character's bags, sorted by in-game order. Enumerated values can contain <c>null</c> when some bag
    /// expansion slots are empty.</summary>
    [Scope(Permission.Inventories)]
    public required IReadOnlyCollection<Bag?>? Bags { get; init; }
}
