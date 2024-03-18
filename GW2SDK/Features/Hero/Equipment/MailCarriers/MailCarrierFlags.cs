namespace GuildWars2.Hero.Equipment.MailCarriers;

/// <summary>Modifiers for mail carriers.</summary>
[PublicAPI]
public sealed record MailCarrierFlags : Flags
{
    /// <summary>Whether the mail carrier is the default for new accounts.</summary>
    public required bool Default { get; init; }
}
