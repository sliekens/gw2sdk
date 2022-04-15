using JetBrains.Annotations;

namespace GW2SDK.Skills.Models;

[PublicAPI]
public sealed record UnblockableSkillFact : SkillFact
{
    public bool Value { get; init; }
}
