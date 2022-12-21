using System;

namespace GuildWars2;

internal sealed class Replica<T> : IReplica<T>
{
    public required T Value { get; init; }

    public required DateTimeOffset Date { get; init; }

    public required DateTimeOffset? Expires { get; init; }

    public required DateTimeOffset? LastModified { get; init; }
}
