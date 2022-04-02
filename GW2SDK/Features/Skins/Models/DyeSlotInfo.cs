using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Skins
{
    [PublicAPI]
    [DataTransferObject]
    public sealed record DyeSlotInfo
    {
        public IReadOnlyCollection<DyeSlot?> Default { get; init; } = Array.Empty<DyeSlot?>();

        public IReadOnlyCollection<DyeSlot?>? AsuraFemale { get; init; }

        public IReadOnlyCollection<DyeSlot?>? AsuraMale { get; init; }

        public IReadOnlyCollection<DyeSlot?>? CharrFemale { get; init; }

        public IReadOnlyCollection<DyeSlot?>? CharrMale { get; init; }

        public IReadOnlyCollection<DyeSlot?>? HumanFemale { get; init; }

        public IReadOnlyCollection<DyeSlot?>? HumanMale { get; init; }

        public IReadOnlyCollection<DyeSlot?>? NornFemale { get; init; }

        public IReadOnlyCollection<DyeSlot?>? NornMale { get; init; }

        public IReadOnlyCollection<DyeSlot?>? SylvariFemale { get; init; }

        public IReadOnlyCollection<DyeSlot?>? SylvariMale { get; init; }
    }
}
