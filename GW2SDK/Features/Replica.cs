using System;

namespace GW2SDK
{
    internal sealed class Replica<T> : IReplica<T>
    {
        public Replica(
            DateTimeOffset date,
            bool hasValue,
            T? value = default,
            DateTimeOffset? expires = null,
            DateTimeOffset? lastModified = null
        )
        {
            Date = date;
            if (hasValue)
            {
                Value = value ?? throw new ArgumentNullException(nameof(value));
                HasValue = true;
                Expires = expires;
                LastModified = lastModified;
            }
        }

        public DateTimeOffset Date { get; }

        public DateTimeOffset? Expires { get; }

        public DateTimeOffset? LastModified { get; }

        public bool HasValue { get; }

        public T? Value { get; }

        public static IReplica<T> NotModified(DateTimeOffset date) => new Replica<T>(date, false);
    }
}
