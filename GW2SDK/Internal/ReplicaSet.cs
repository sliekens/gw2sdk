using System;
using System.Collections;
using System.Collections.Generic;

namespace GuildWars2;

internal sealed class ReplicaSet<T> : IReplicaSet<T>
{
    public required IReadOnlyCollection<T> Values { get; init; }

    public required ICollectionContext Context { get; init; }

    public required DateTimeOffset Date { get; init; }

    public required DateTimeOffset? Expires { get; init; }

    public required DateTimeOffset? LastModified { get; init; }

    public IEnumerator<T> GetEnumerator() => Values.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)Values).GetEnumerator();

    public int Count => Values.Count;
}
