using System;
using System.Collections;
using System.Collections.Generic;

namespace GuildWars2;

internal sealed class ReplicaPage<T> : IReplicaPage<T>
{
    public ReplicaPage(
        DateTimeOffset date,
        IReadOnlyCollection<T> values,
        IPageContext context,
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

    public IReadOnlyCollection<T> Values { get; }

    public IPageContext Context { get; }

    public DateTimeOffset Date { get; }

    public DateTimeOffset? Expires { get; }

    public DateTimeOffset? LastModified { get; }

    public IEnumerator<T> GetEnumerator() => Values.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)Values).GetEnumerator();

    public int Count => Values.Count;
}
