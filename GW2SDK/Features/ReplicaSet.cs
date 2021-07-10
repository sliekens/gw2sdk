using System;
using System.Collections.Generic;

namespace GW2SDK
{
    internal sealed class ReplicaSet<T> : IReplicaSet<T>
    {
        public ReplicaSet(
            DateTimeOffset date,
            bool hasValues,
            IReadOnlySet<T>? value = default,
            ICollectionContext? context = null,
            DateTimeOffset? expires = null,
            DateTimeOffset? lastModified = null
        )
        {

            Date = date;
            if (hasValues)
            {
                Values = value ?? throw new ArgumentNullException(nameof(value));
                Context = context ?? throw new ArgumentNullException(nameof(context));
                HasValues = true;
                Expires = expires;
                LastModified = lastModified;
            }
        }

        public DateTimeOffset Date { get; }

        public DateTimeOffset? Expires { get; }

        public DateTimeOffset? LastModified { get; }

        public bool HasValues { get; }

        public IReadOnlySet<T>? Values { get; }

        public ICollectionContext? Context { get; }
    }
}
