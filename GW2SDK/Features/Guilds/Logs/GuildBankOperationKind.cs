namespace GuildWars2.Guilds.Logs;

/// <summary>The kinds of guild bank operations.</summary>
[PublicAPI]
public enum GuildBankOperationKind
{
    /// <summary>Used when items or coins are deposited into the guild bank.</summary>
    Deposit = 1,

    /// <summary>Used when items or coins are withdrawn from the guild bank.</summary>
    Withdraw,

    /// <summary>Used when items are moved within the guild bank.</summary>
    Move
}
