using GW2SDK.Annotations;
using GW2SDK.Professions;
using JetBrains.Annotations;

namespace GW2SDK.Accounts.Characters;

[PublicAPI]
[DataTransferObject]
public sealed record TrainingTrack
{
    /// <summary>The ID of the current training track. Related data can be resolved from <see cref="Profession.Training" />.</summary>
    public int Id { get; init; }

    /// <summary>The number of hero points spent in this track.</summary>
    public int Spent { get; init; }

    /// <summary>Whether the current training is completed.</summary>
    public bool Done { get; init; }
}
