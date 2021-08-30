using System;
using System.Collections.Generic;

namespace GW2SDK
{
    internal sealed class ReplicaSet<T> : IReplicaSet<T>
    {
        public ReplicaSet(
            DateTimeOffset date,
            bool hasValues,
#if NET
            IReadOnlySet<T>? value = default,
#else
            IReadOnlyCollection<T>? value = default,
#endif
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

#if NET
        public IReadOnlySet<T>? Values { get; }

        public ICollectionContext? Context { get; }
#else
        public IReadOnlyCollection<T> Values { get; } = default!;

        public ICollectionContext Context { get; } = default!;
#endif
    }
}
