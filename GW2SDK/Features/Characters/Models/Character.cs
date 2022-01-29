using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Characters
{
    [PublicAPI]
    [DataTransferObject]
    [Scope(Permission.Account, Permission.Characters)]
    public sealed record Character
    {
        /// <summary>The name of the current character.</summary>
        /// <remarks>This can be changed later with a Black Lion contract.</remarks>
        public string Name { get; init; } = "";

        /// <summary>The race selected during creation of the current character.</summary>
        public Race Race { get; init; }

        /// <summary>Whether the character is male or female.</summary>
        /// <remarks>This can be changed later with a Black Lion contract.</remarks>
        public Gender Gender { get; init; }

        /// <summary>Additional facts about the current character that did not fit anywhere else.</summary>
        public CharacterFlag[] Flags { get; init; } = Array.Empty<CharacterFlag>();

        /// <summary>The profession name of the current character.</summary>
        public ProfessionName Profession { get; init; }

        public int Level { get; init; }

        /// <summary>The current guild, or an empty string if the character is not currently representing a guild.</summary>
        public string GuildId { get; init; } = "";

        /// <summary>The amount of time played as the current character.</summary>
        public TimeSpan Age { get; init; }

        /// <summary>The date and time when this information was last updated.</summary>
        /// <remarks>This is roughly equal to the last time played as the current character.</remarks>
        public DateTimeOffset LastModified { get; init; }

        /// <summary>The date and time when the current character was created.</summary>
        public DateTimeOffset Created { get; init; }

        /// <summary>The number of times the current character was fully defeated.</summary>
        public int Deaths { get; init; }

        public IEnumerable<CraftingDiscipline> CraftingDisciplines { get; init; } = Array.Empty<CraftingDiscipline>();

        /// <summary>The selected title ID of the current character.</summary>
        public int? TitleId { get; init; }

        /// <summary>The IDs of the answers to backstory questions that were selected during creation of the current character.</summary>
        public string[] Backstory { get; init; } = Array.Empty<string>();

        /// <summary>The trained WvW abilities and their rank.</summary>
        [Scope(Permission.Progression)]
        public IEnumerable<WvwAbility>? WvwAbilities { get; init; }

        /// <summary>The number of build tabs available to the current character.</summary>
        [Scope(Permission.Builds)]
        public int? BuildTabsUnlocked { get; init; }

        /// <summary>The active build tab of the current character.</summary>
        /// <remarks>This starts counting at 1, do not use as a collection index.</remarks>
        [Scope(Permission.Builds)]
        public int? ActiveBuildTab { get; init; }

        /// <summary>All the build tabs of the current character.</summary>
        [Scope(Permission.Builds)]
        public IEnumerable<BuildTab>? BuildTabs { get; init; }

        /// <summary>The number of equipment tabs available to the current character.</summary>
        [Scope(ScopeRequirement.Any, Permission.Builds, Permission.Inventories)]
        public int? EquipmentTabsUnlocked { get; init; }

        /// <summary>The active equipment tab of the current character.</summary>
        /// <remarks>This starts counting at 1, do not use as a collection index.</remarks>
        [Scope(ScopeRequirement.Any, Permission.Builds, Permission.Inventories)]
        public int? ActiveEquipmentTab { get; init; }

        /// <summary>All the equipment in the current character's armory.</summary>
        [Scope(ScopeRequirement.Any, Permission.Builds, Permission.Inventories)]
        public IEnumerable<EquipmentItem>? Equipment { get; init; }

        /// <summary>All the equipment tabs of the current character.</summary>
        [Scope(ScopeRequirement.Any, Permission.Builds, Permission.Inventories)]
        public IEnumerable<EquipmentTab>? EquipmentTabs { get; init; }

        /// <summary>The IDs of the recipes that the current character has unlocked.</summary>
        /// <summary>This includes unlocked recipes that are unavailable to the character's active crafting disciplines.</summary>
        [Scope(Permission.Inventories)]
        public IEnumerable<int>? Recipes { get; init; }

        /// <summary>The current character's hero point progression.</summary>
        [Scope(Permission.Builds)]
        public IEnumerable<TrainingTrack>? Training { get; init; }

        /// <summary>The current character's bags, sorted by in-game order. Enumerated values can contain <c>null</c> when some bag
        /// expansion slots are empty.</summary>
        [Scope(Permission.Inventories)]
        public IEnumerable<Bag?>? Bags { get; init; }
    }
}
