using System;

namespace GW2SDK;

internal sealed class Replica<T> : IReplica<T>
{
    public Replica(
        DateTimeOffset date,
        T value,
        DateTimeOffset? expires = null,
        DateTimeOffset? lastModified = null
    )
    {
        Date = date;
        Value = value;
        Expires = expires;
        LastModified = lastModified;
    }

    public DateTimeOffset Date { get; }

    public DateTimeOffset? Expires { get; }

    public DateTimeOffset? LastModified { get; }

    public T Value { get; }
}
