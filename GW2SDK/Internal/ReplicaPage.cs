using System;
using System.Collections;
using System.Collections.Generic;

namespace GuildWars2;

internal sealed class ReplicaPage<T> : IReplicaPage<T>
{
    public required IReadOnlyCollection<T> Values { get; init; }

    public required IPageContext Context { get; init; }

    public required DateTimeOffset Date { get; init; }

    public required DateTimeOffset? Expires { get; init; }

    public required DateTimeOffset? LastModified { get; init; }

    public IEnumerator<T> GetEnumerator() => Values.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)Values).GetEnumerator();

    public int Count => Values.Count;
}
