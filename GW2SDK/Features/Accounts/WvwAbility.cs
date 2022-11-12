using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Accounts;

[PublicAPI]
[DataTransferObject]
public sealed record WvwAbility
{
    /// <summary>The ID of the current ability.</summary>
    public required int Id { get; init; }

    /// <summary>The current rank of the ability.</summary>
    public required int Rank { get; init; }
}
