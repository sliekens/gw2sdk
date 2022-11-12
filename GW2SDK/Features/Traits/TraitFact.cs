using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Traits;

[PublicAPI]
[Inheritable]
[DataTransferObject]
public record TraitFact
{
    public required string Text { get; init; }

    public required string Icon { get; init; }
}
