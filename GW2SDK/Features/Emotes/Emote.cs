using System.Collections.Generic;
using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Emotes;

[PublicAPI]
[DataTransferObject]
public sealed record Emote
{
    public required string Id { get; init; }

    public required IReadOnlyCollection<string> Commands { get; init; }

    public required IReadOnlyCollection<int> UnlockItems { get; init; }
}
