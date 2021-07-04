using System;

namespace GW2SDK
{
    internal sealed class Replica<T> : IReplica<T>
    {
        public Replica(
            bool hasValue,
            DateTimeOffset? update = null,
            T? value = default,
            DateTimeOffset? expires = null,
            DateTimeOffset? lastModified = null
        )
        {
            HasValue = hasValue;
            Update = update;
            if (hasValue)
            {
                Value = value ?? throw new ArgumentNullException(nameof(value));
                Expires = expires;
                LastModified = lastModified;
            }
        }

        public DateTimeOffset? Update { get; }

        public DateTimeOffset? Expires { get; }

        public DateTimeOffset? LastModified { get; }

        public bool HasValue { get; }

        public T? Value { get; }

        public static IReplica<T> NotModified(DateTimeOffset? date) => new Replica<T>(false, date);
    }
}
