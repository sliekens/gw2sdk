using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Characters
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record Build
    {
        /// <summary>The player-chosen name of this build.</summary>
        public string Name { get; init; } = "";

        public ProfessionName Profession { get; init; }

        // Always length 3
        public IReadOnlyCollection<Specialization> Specializations { get; init; } = Array.Empty<Specialization>();

        public SkillBar Skills { get; init; } = new();

        public SkillBar AquaticSkills { get; init; } = new();

        public PetSkillBar? Pets { get; init; }

        // Always length 2 or missing
        public IReadOnlyCollection<string?>? Legends { get; init; }

        // Always length 2 or missing
        public IReadOnlyCollection<string?>? AquaticLegends { get; init; }
    }
}
