using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Emotes;

[PublicAPI]
[DataTransferObject]
public sealed record Emote
{
    public string Id { get; init; } = "";

    public IReadOnlyCollection<string> Commands { get; init; } = Array.Empty<string>();

    public IReadOnlyCollection<int> UnlockItems { get; init; } = Array.Empty<int>();
}
