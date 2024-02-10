namespace GuildWars2.Hero.Equipment.MailCarriers;

[PublicAPI]
[DataTransferObject]
public sealed record MailCarrier
{
    public required int Id { get; init; }

    /// <summary>The IDs of the items that unlock the mail carrier when consumed.</summary>
    public required IReadOnlyCollection<int> UnlockItemIds { get; init; }

    /// <summary>The display order of the mail carrier in the equipment panel.</summary>
    public required int Order { get; init; }

    public required string IconHref { get; init; }

    public required string Name { get; init; }

    public required MailCarrierFlags Flags { get; init; }
}
