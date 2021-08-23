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
        public string Name { get; init; } = "";

        public Race Race { get; init; }

        public Gender Gender { get; init; }

        public CharacterFlag[] Flags { get; init; } = Array.Empty<CharacterFlag>();

        public ProfessionName Profession { get; init; }

        public int Level { get; init; }

        /// <summary>Gets the current guild, or an empty string if the character is not currently representing a guild.</summary>
        public string GuildId { get; init; } = "";

        public TimeSpan Age { get; init; }

        public DateTimeOffset LastModified { get; init; }

        public DateTimeOffset Created { get; init; }

        public int Deaths { get; init; }

        public IEnumerable<CraftingDiscipline> CraftingDisciplines { get; init; } = Array.Empty<CraftingDiscipline>();

        public int? TitleId { get; init; }

        public string[] Backstory { get; init; } = Array.Empty<string>();

        [Scope(Permission.Progression)]
        public IEnumerable<WvwAbility>? WvwAbilities { get; init; }

        [Scope(Permission.Builds)]
        public int? BuildTabsUnlocked { get; init; }

        [Scope(Permission.Builds)]
        public int? ActiveBuildTab { get; init; }

        [Scope(Permission.Builds)]
        public IEnumerable<BuildTab>? BuildTabs { get; init; }

        [Scope(ScopeRequirement.Any, Permission.Builds, Permission.Inventories)]
        public int? EquipmentTabsUnlocked { get; init; }

        [Scope(ScopeRequirement.Any, Permission.Builds, Permission.Inventories)]
        public int? ActiveEquipmentTab { get; init; }

        [Scope(ScopeRequirement.Any, Permission.Builds, Permission.Inventories)]
        public IEnumerable<EquipmentItem>? Equipment { get; init; }

        [Scope(ScopeRequirement.Any, Permission.Builds, Permission.Inventories)]
        public IEnumerable<EquipmentTab>? EquipmentTabs { get; init; }

        [Scope(Permission.Inventories)]
        public IEnumerable<int>? Recipes { get; init; }

        [Scope(Permission.Builds)]
        public IEnumerable<TrainingObjective>? Training { get; init; }

        /// <summary>The current character's bags, sorted by in-game order. Enumerated values can contain <c>null</c> when some bag
        /// expansion slots are empty.</summary>
        [Scope(Permission.Inventories)]
        public IEnumerable<Bag?>? Bags { get; init; }
    }
}
