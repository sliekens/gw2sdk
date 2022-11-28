using GuildWars2.Annotations;
using GuildWars2.BuildStorage;
using JetBrains.Annotations;

namespace GuildWars2.Accounts;

[PublicAPI]
[DataTransferObject]
public sealed record BuildTab
{
    /// <summary>The number of the current tab.</summary>
    public required int Tab { get; init; }

    /// <summary>The selected skills and traits on the current build tab.</summary>
    public required Build Build { get; init; }
}
