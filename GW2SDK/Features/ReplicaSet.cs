using System;
using System.Collections.Generic;

namespace GW2SDK
{
    internal sealed class ReplicaSet<T> : IReplicaSet<T>
    {
        public ReplicaSet(
            bool hasValue,
            DateTimeOffset? update = null,
            IReadOnlySet<T>? value = default,
            ICollectionContext? context = null,
            DateTimeOffset? expires = null,
            DateTimeOffset? lastModified = null)
        {

            HasValues = hasValue;
            Update = update;
            if (hasValue)
            {
                Values = value ?? throw new ArgumentNullException(nameof(value));
                Context = context ?? throw new ArgumentNullException(nameof(context));
                Expires = expires;
                LastModified = lastModified;
            }
        }

        public DateTimeOffset? Update { get; }

        public DateTimeOffset? Expires { get; }

        public DateTimeOffset? LastModified { get; }

        public bool HasValues { get; }

        public IReadOnlySet<T>? Values { get; }

        public ICollectionContext? Context { get; }
    }
}
