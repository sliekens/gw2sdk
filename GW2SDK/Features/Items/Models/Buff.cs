using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Items;

[PublicAPI]
[DataTransferObject]
public sealed record Buff
{
    public required int SkillId { get; init; }

    public required string Description { get; init; }
}
