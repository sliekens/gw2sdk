using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Masteries;

[PublicAPI]
[DataTransferObject]
public sealed record MasteryProgress
{
    public required int Id { get; init; }

    public required int Level { get; init; }
}
