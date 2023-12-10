namespace GuildWars2.Hero.Equipment.MailCarriers;

[PublicAPI]
[DataTransferObject]
public sealed record MailCarrier
{
    public required int Id { get; init; }

    public required IReadOnlyCollection<int> UnlockItems { get; init; }

    public required int Order { get; init; }

    public required string IconHref { get; init; }

    public required string Name { get; init; }

    public required MailCarrierFlags Flags { get; init; }
}
