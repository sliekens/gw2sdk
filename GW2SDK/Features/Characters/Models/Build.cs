using System;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Characters
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record Build
    {
        public string Name { get; init; } = "";

        public ProfessionName Profession { get; init; }

        // Always length 3
        public Specialization[] Specializations { get; init; } = Array.Empty<Specialization>();

        public SkillBar Skills { get; init; } = new();

        public SkillBar AquaticSkills { get; init; } = new();

        public PetSkillBar? Pets { get; init; }

        // Always length 2 or missing
        public string?[]? Legends { get; init; }

        // Always length 2 or missing
        public string?[]? AquaticLegends { get; init; }
    }
}