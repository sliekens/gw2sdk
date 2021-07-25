using System;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Specializations
{
    [PublicAPI]
    [DataTransferObject]
    public sealed record Specialization
    {
        public int Id { get; init; }

        public string Name { get; init; } = "";

        public ProfessionName Profession { get; init; }

        public bool Elite { get; init; }

        public int[] MinorTraits { get; init; } = Array.Empty<int>();

        public int[] MajorTraits { get; init; } = Array.Empty<int>();

        public int? WeaponTrait { get; init; }

        public string Icon { get; init; } = "";

        public string Background { get; init; } = "";

        public string ProfessionIconBig { get; set; } = "";

        public string ProfessionIcon { get; set; } = "";
    }
}
