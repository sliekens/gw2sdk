using System;
using System.Collections;
using System.Collections.Generic;

namespace GuildWars2;

internal sealed class ReplicaSet<T> : IReplicaSet<T>
{
    public ReplicaSet(
        DateTimeOffset date,
        IReadOnlyCollection<T> values,
        ICollectionContext context,
        DateTimeOffset? expires = null,
        DateTimeOffset? lastModified = null
    )
    {
        Date = date;
        Values = values;
        Context = context;
        Expires = expires;
        LastModified = lastModified;
    }

    public DateTimeOffset Date { get; }

    public DateTimeOffset? Expires { get; }

    public DateTimeOffset? LastModified { get; }

    public IReadOnlyCollection<T> Values { get; }

    public ICollectionContext Context { get; }

    public IEnumerator<T> GetEnumerator() => Values.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)Values).GetEnumerator();

    public int Count => Values.Count;
}
