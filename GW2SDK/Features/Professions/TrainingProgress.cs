using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Professions;

[PublicAPI]
[DataTransferObject]
public sealed record TrainingProgress
{
    public required int Id { get; init; }

    public required int Spent { get; init; }

    public required bool Done { get; init; }
}
