using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Traits;

[PublicAPI]
[Inheritable]
[DataTransferObject]
public record TraitFact
{
    public required string Text { get; init; }

    public required string Icon { get; init; }
}
