using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Mounts;

[PublicAPI]
[DataTransferObject]
public sealed record SkillReference
{
    public required int Id { get; init; }

    public required SkillSlot Slot { get; init; }
}
