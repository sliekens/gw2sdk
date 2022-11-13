using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Emotes;

[PublicAPI]
[DataTransferObject]
public sealed record Emote
{
    public required string Id { get; init; }

    public required IReadOnlyCollection<string> Commands { get; init; }

    public required IReadOnlyCollection<int> UnlockItems { get; init; }
}
