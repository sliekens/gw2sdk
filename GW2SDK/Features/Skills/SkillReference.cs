using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Skills;

[PublicAPI]
[DataTransferObject]
public sealed record SkillReference
{
    public required int Id { get; init; }

    public required Attunement? Attunement { get; init; }

    public required Transformation? Form { get; init; }
}
