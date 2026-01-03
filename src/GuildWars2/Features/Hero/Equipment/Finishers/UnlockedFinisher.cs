using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Finishers;

/// <summary>Information about a finisher unlocked on the account.</summary>
[DataTransferObject]
[JsonConverter(typeof(UnlockedFinisherJsonConverter))]
public sealed record UnlockedFinisher
{
    /// <summary>The finisher ID.</summary>
    public required int Id { get; init; }

    /// <summary>Indicates whether the finisher is permanent.</summary>
    public required bool Permanent { get; init; }

    /// <summary>How many uses of the finisher are left on the account, or <c>null</c> if the finisher is permanent.</summary>
    public required int? Quantity { get; init; }
}
