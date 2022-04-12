using JetBrains.Annotations;

namespace GW2SDK.Skills;

[PublicAPI]
public sealed record ComboFieldSkillFact : SkillFact
{
    public ComboFieldName Field { get; init; }
}