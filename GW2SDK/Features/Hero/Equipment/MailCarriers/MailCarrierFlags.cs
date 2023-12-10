namespace GuildWars2.Hero.Equipment.MailCarriers;

/// <summary>Modifiers for mail carriers.</summary>
[PublicAPI]
public sealed record MailCarrierFlags
{
    /// <summary>Whether the mail carrier is the default for new accounts.</summary>
    public required bool Default { get; init; }

    /// <summary>Other undocumented flags. If you find out what they mean, please open an issue or a pull request.</summary>
    public required IReadOnlyCollection<string> Other { get; init; }
}
