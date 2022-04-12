using System;
using System.Collections;
using System.Collections.Generic;

namespace GW2SDK;

internal sealed class ReplicaSet<T> : IReplicaSet<T>
{
    public ReplicaSet(
        DateTimeOffset date,
#if NET
        IReadOnlySet<T> values,
#else
        IReadOnlyCollection<T> values,
#endif
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

#if NET
    public IReadOnlySet<T> Values { get; }
#else
    public IReadOnlyCollection<T> Values { get; }
#endif

    public ICollectionContext Context { get; }

    public IEnumerator<T> GetEnumerator()
    {
        return Values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)Values).GetEnumerator();
    }

    public int Count => Values.Count;
}