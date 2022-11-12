using JetBrains.Annotations;

namespace GW2SDK.Skills;

[PublicAPI]
public sealed record ComboFinisherSkillFact : SkillFact
{
    public required int Percent { get; init; }

    public required ComboFinisherName FinisherName { get; init; }
}
