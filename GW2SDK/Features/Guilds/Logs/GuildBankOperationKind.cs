using System.ComponentModel;
using System.Text.Json.Serialization;

namespace GuildWars2.Guilds.Logs;

/// <summary>The kinds of guild bank operations.</summary>
[PublicAPI]
[DefaultValue(None)]
[JsonConverter(typeof(GuildBankOperationKindJsonConverter))]
public enum GuildBankOperationKind
{
    /// <summary>No specific operation or unknown operation.</summary>
    None,

    /// <summary>Used when items or coins are deposited into the guild bank.</summary>
    Deposit,

    /// <summary>Used when items or coins are withdrawn from the guild bank.</summary>
    Withdraw,

    /// <summary>Used when items are moved within the guild bank.</summary>
    Move
}
