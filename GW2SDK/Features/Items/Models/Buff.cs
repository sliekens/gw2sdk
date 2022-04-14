using GW2SDK.Annotations;

using JetBrains.Annotations;

namespace GW2SDK.Items.Models;

[PublicAPI]
[DataTransferObject]
public sealed record Buff
{
    public int SkillId { get; init; }

    public string Description { get; init; } = "";
}