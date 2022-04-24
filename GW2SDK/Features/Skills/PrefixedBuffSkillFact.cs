using JetBrains.Annotations;

namespace GW2SDK.Skills;

[PublicAPI]
public sealed record PrefixedBuffSkillFact : BuffSkillFact
{
    public BuffPrefix Prefix { get; init; } = new();
}
