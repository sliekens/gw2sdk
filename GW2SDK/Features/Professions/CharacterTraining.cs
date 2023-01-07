using System.Collections.Generic;
using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Professions;

[PublicAPI]
[DataTransferObject]
public sealed record CharacterTraining
{
    public required IReadOnlyCollection<TrainingProgress> Training { get; init; }
}