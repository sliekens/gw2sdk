using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Items;

[PublicAPI]
[DataTransferObject]
public sealed record Buff
{
    public required int SkillId { get; init; }

    public required string Description { get; init; }
}
