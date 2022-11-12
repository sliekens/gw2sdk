using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Skins;

[PublicAPI]
[DataTransferObject]
public sealed record DyeSlotInfo
{
    public required IReadOnlyCollection<DyeSlot?> Default { get; init; }

    public required IReadOnlyCollection<DyeSlot?>? AsuraFemale { get; init; }

    public required IReadOnlyCollection<DyeSlot?>? AsuraMale { get; init; }

    public required IReadOnlyCollection<DyeSlot?>? CharrFemale { get; init; }

    public required IReadOnlyCollection<DyeSlot?>? CharrMale { get; init; }

    public required IReadOnlyCollection<DyeSlot?>? HumanFemale { get; init; }

    public required IReadOnlyCollection<DyeSlot?>? HumanMale { get; init; }

    public required IReadOnlyCollection<DyeSlot?>? NornFemale { get; init; }

    public required IReadOnlyCollection<DyeSlot?>? NornMale { get; init; }

    public required IReadOnlyCollection<DyeSlot?>? SylvariFemale { get; init; }

    public required IReadOnlyCollection<DyeSlot?>? SylvariMale { get; init; }
}
