using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Skins
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record DyeSlotInfo
    {
        public DyeSlot?[] Default { get; init; } = new DyeSlot?[0];

        public DyeSlot?[]? AsuraFemale { get; init; }

        public DyeSlot?[]? AsuraMale { get; init; }

        public DyeSlot?[]? CharrFemale { get; init; }

        public DyeSlot?[]? CharrMale { get; init; }

        public DyeSlot?[]? HumanFemale { get; init; }

        public DyeSlot?[]? HumanMale { get; init; }

        public DyeSlot?[]? NornFemale { get; init; }

        public DyeSlot?[]? NornMale { get; init; }

        public DyeSlot?[]? SylvariFemale { get; init; }

        public DyeSlot?[]? SylvariMale { get; init; }
    }
}
