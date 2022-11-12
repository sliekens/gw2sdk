using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Skills;

[PublicAPI]
[Inheritable]
[DataTransferObject]
public record SkillFact
{
    public required string Text { get; init; }

    public required string Icon { get; init; }
}
