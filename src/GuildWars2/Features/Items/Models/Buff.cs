using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a buff, which is a passive effect that enhances your stats or abilities.</summary>
[DataTransferObject]
[JsonConverter(typeof(BuffJsonConverter))]
public sealed record Buff
{
    /// <summary>The skill ID associated with the buff. It is always a passive skill which grants the passive effect to the
    /// player.</summary>
    /// <remarks>Unfortunately, these skills are hidden from the API, so you can't look them up.</remarks>
    public required int SkillId { get; init; }

    /// <summary>The effect description.</summary>
    public required string Description { get; init; }
}
