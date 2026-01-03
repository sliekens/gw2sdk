using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.MailCarriers;

/// <summary>Information about a mail carrier.</summary>
[DataTransferObject]
[JsonConverter(typeof(MailCarrierJsonConverter))]
public sealed record MailCarrier
{
    /// <summary>The mail carrier ID.</summary>
    public required int Id { get; init; }

    /// <summary>The IDs of the items that unlock the mail carrier when consumed.</summary>
    public required IReadOnlyCollection<int> UnlockItemIds { get; init; }

    /// <summary>The display order of the mail carrier in the equipment panel.</summary>
    public required int Order { get; init; }

    /// <summary>The URL of the mail carrier icon.</summary>
    [Obsolete("Use IconUrl instead.")]
    public required string IconHref { get; init; }

    /// <summary>The URL of the mail carrier icon.</summary>
    public required Uri IconUrl { get; init; }

    /// <summary>The name of the mail carrier.</summary>
    public required string Name { get; init; }

    /// <summary>Contains various modifiers.</summary>
    public required MailCarrierFlags Flags { get; init; }
}
